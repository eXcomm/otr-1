﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OffTheRecord.Tests {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class FileHandlingResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal FileHandlingResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("OffTheRecord.Tests.FileHandlingResource", typeof(FileHandlingResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to marshal2	marshal3@irc.freenode.net	prpl-irc	80724d46d9d906a28af31d15adfd510822ac3fd9	verified
        ///marshal3	marshal2@irc.freenode.net	prpl-irc	18dcd190ccaad02aed74e69d3b96355e61a82b3e	verified
        ///test123_4	testuser2@irc.freenode.net	prpl-irc	64bfb577c9591b3dbb6b697599f572ce7d1ffc9d	smp
        ///testuser2	test123_4@irc.freenode.net	prpl-irc	51f2e7db2a0c14facd568107aceaae73f362c869	smp
        ///.
        /// </summary>
        internal static string otr_fingerprints {
            get {
                return ResourceManager.GetString("otr_fingerprints", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to # WARNING! You shouldn&apos;t copy this file to another computer. It is unnecessary and can cause problems.
        ///testuser2@irc.freenode.net	prpl-irc	299c2916
        ///test123_4@irc.freenode.net	prpl-irc	8cf547f1
        ///marshal3@irc.freenode.net	prpl-irc	4b2bf242
        ///marshal2@irc.freenode.net	prpl-irc	f2e0ee97
        ///.
        /// </summary>
        internal static string otr_instance_tags {
            get {
                return ResourceManager.GetString("otr_instance_tags", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (privkeys
        /// (account
        ///(name &quot;alice@domain.com&quot;)
        ///(protocol prpl-msn)
        ///(private-key 
        /// (dsa 
        ///  (p #00AEC0FBB4CEA96EF8BDD0E91D1BA2F6641B6535CBDA8D739CC2898FE7B472865AB60AD2B1BAA2368603C7439E63BC2F2F33D422E70173F70DB738DF5979EAEAF3CAC343CBF711960E16786703C80DF0734D8330DC955DA84B521DAB5C729202F1244D805E6BF2CC7A7142CAD74BE5FFFC14B9CCB6CABB7DB10A8F2DDB4E82383F#)
        ///  (q #00A2A2BC20E2D94C44C63608479C79068CE7914EF3#)
        ///  (g #69B9FC5A73F3F6EA3A86F8FA3A203F42DACDC3A1516002025E5765A9DCB975F348ACBBA2116230E19CE3FC5256546 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string otr_private_key {
            get {
                return ResourceManager.GetString("otr_private_key", resourceCulture);
            }
        }
    }
}
