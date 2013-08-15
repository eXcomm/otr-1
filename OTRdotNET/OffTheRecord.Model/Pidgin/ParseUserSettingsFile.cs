namespace OffTheRecord.Model.Pidgin
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using OffTheRecord.Model.Pidgin.UserSettingsFile;
    #endregion

    public static class ParseUserSettingsFile
    {
        #region Public methods
        public static privkeys Deserialize(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            string data = File.ReadAllText(filename);

            // build parse tree;
            Item root = ParseUserSettingsFile.BuildTree(data);

            return privkeys.Deserialize(root);
        }

        public static string Serialize(privkeys privkeys)
        {
            return privkeys.Serialize();
        }
        #endregion

        #region Private methods
        private static Item BuildTree(string input)
        {
            Item parent = new Item();

            if (input.Length == 0)
            {
                return parent;
            }

            return BuildTree(input, parent);
        }

        private static Item BuildTree(string input, Item parent)
        {
            int start = -1;
            int nested = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    if (start == -1)
                    {
                        start = i;
                    }
                    else
                    {
                        nested++;
                    }
                }
                else if (input[i] == ')')
                {
                    if (nested == 0)
                    {
                        if (start != -1)
                        {
                            string value = input.Substring(start, i - start);
                            string content = input.Substring(start + 1, i - start -2);

                            Item child = new Item();
                            child.Value = value;
                            child.Parent = parent;

                            BuildTree(content, child);

                            parent.Children.Add(child);

                            start = -1;
                            nested = 0;
                        }
                    }
                    else
                    {
                        nested--;
                    }
                }
            }

            return parent;
        }
        #endregion
    }
}
