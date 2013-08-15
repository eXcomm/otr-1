// <copyright>
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace System.ComponentModel.Composition.Hosting
{
    using System;
    using System.ComponentModel.Composition.Primitives;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using OffTheRecord.DependencyInjection;

    /// <summary>
    /// The global host used to provide a <see cref="CompositionContainer"/> for the <see cref="CompositionInitializer"/>.
    /// </summary>
    public static class CompositionHost
    {
        /// <summary>
        /// An internal synchronization object.
        /// </summary>
        private static readonly object LockObject = new object();

        /// <summary>
        /// The global <see cref="CompositionContainer"/>.
        /// </summary>
        private static CompositionContainer globalContainer;

        /// <summary>
        ///     This method can be used to initialize the global container used by <see cref="CompositionInitializer.SatisfyImports"/>
        ///     in case where the default container doesn't provide enough flexibility. 
        ///     <para />
        ///     If this method is needed it should be called exactly once and as early as possible in the application host. It will need
        ///     to be called before the first call to <see cref="CompositionInitializer.SatisfyImports"/>.
        /// </summary>
        /// <param name="container">
        ///     <see cref="CompositionContainer"/> that should be used instead of the default global container.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="container"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     Either <see cref="Initialize(CompositionContainer)" /> or <see cref="Initialize(ComposablePartCatalog[])" />has already been called or someone has already made use of the global 
        ///     container via <see cref="CompositionInitializer.SatisfyImports(object)"/>. In either case you need to ensure that it 
        ///     is called only once and that it is called early in the application host startup code.
        /// </exception>
        public static void Initialize(CompositionContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            CompositionContainer newContainer;
            bool alreadyCreated = TryGetOrCreateContainer(() => container, out newContainer);

            if (alreadyCreated)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionStrings.InvalidOperationException_GlobalContainerAlreadyInitialized));
            }
        }

        /// <summary>
        /// This method can be used to initialize the global container used by <see cref="CompositionInitializer.SatisfyImports(object)"/>
        ///     in case where the default container doesn't provide enough flexibility. 
        ///     <para />
        ///     If this method is needed it should be called exactly once and as early as possible in the application host. It will need
        ///     to be called before the first call to <see cref="CompositionInitializer.SatisfyImports(object)"/>.
        /// </summary>
        /// <param name="catalogs">
        /// An array of <see cref="ComposablePartCatalog"/> that should be used to initialize the <see cref="CompositionContainer"/> with.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="catalogs"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Either <see cref="Initialize(CompositionContainer)"/> or <see cref="Initialize(ComposablePartCatalog[])"/>has already been called or someone has already made use of the global 
        ///     container via <see cref="CompositionInitializer.SatisfyImports(object)"/>. In either case you need to ensure that it 
        ///     is called only once and that it is called early in the application host startup code.
        /// </exception>
        /// <returns>
        /// The initialized <see cref="CompositionContainer"/>.
        /// </returns>
        public static CompositionContainer Initialize(params ComposablePartCatalog[] catalogs)
        {
            AggregateCatalog aggregateCatalog = new AggregateCatalog(catalogs);
            CompositionContainer container = new CompositionContainer(aggregateCatalog);
            try
            {
                CompositionHost.Initialize(container);
            }
            catch
            {
                container.Dispose();

                // NOTE : this is important, as this prevents the disposal of the catalogs passed as input arguments
                aggregateCatalog.Catalogs.Clear();
                aggregateCatalog.Dispose();

                throw;
            }

            return container;
        }

        /// <summary>
        /// Gets or creates the global <see cref="CompositionContainer" />.
        /// </summary>
        /// <param name="createContainer">The create container.</param>
        /// <param name="globalContainer">The global container.</param>
        /// <returns>True if the new container was created previously.</returns>
        internal static bool TryGetOrCreateContainer(Func<CompositionContainer> createContainer, out CompositionContainer globalContainer)
        {
            bool alreadyCreated = true;
            if (CompositionHost.globalContainer == null)
            {
                var container = createContainer.Invoke();
                lock (LockObject)
                {
                    if (CompositionHost.globalContainer == null)
                    {
                        Thread.MemoryBarrier();
                        CompositionHost.globalContainer = container;
                        alreadyCreated = false;
                    }
                }
            }

            globalContainer = CompositionHost.globalContainer;
            return alreadyCreated;
        }

        /// <summary>
        /// Creates the default <see cref="ComposablePartCatalog" /> for the <see cref="CompositionInitializer"/>.
        /// </summary>
        /// <returns>The default <see cref="ComposablePartCatalog" />.</returns>
        internal static ComposablePartCatalog CreateDefaultCatalog()
        {
            var catalog = new AggregateCatalog();

            string startup = (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).Location;
            startup = System.IO.Path.GetDirectoryName(startup);
            catalog.Catalogs.Add(new DirectoryCatalog(startup));
            catalog.Catalogs.Add(new DirectoryCatalog(startup, "*.exe"));

            var directory = System.IO.Path.Combine(startup, @"Extensions");
            if (Directory.Exists(directory))
            {
                catalog.Catalogs.Add(new DirectoryCatalog(directory));
            }

            return catalog;
        }
    }
}
