namespace OffTheRecord.Model.Pidgin.UserSettingsFile
{
    #region namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    #endregion

    public sealed class dsa
    {
        #region constructor
        public dsa()
        {
        }

        public dsa(string p, string q, string g, string y, string x)
        {
            this.p = p;
            this.q = q;
            this.g = g;
            this.y = y;
            this.x = x;
        }
        #endregion

        #region Public properties
        public string p { get; set; }
        public string q { get; set; }
        public string g { get; set; }
        public string y { get; set; }
        public string x { get; set; }
        #endregion

        #region Internal methods
        internal string Serialize()
        {
            return string.Format(" (dsa{0}  (p #{1}#){0}  (q #{2}#){0}  (g #{3}#){0}  (y #{4}#){0}  (x #{5}#){0}  ){0}", Environment.NewLine, this.p, this.q, this.g, this.y, this.x);
        }

        internal static dsa Deserialize(Item item)
        {
            if (!item.Value.StartsWith("(dsa"))
            {
                throw new ArgumentException("incorrect format");
            }

            dsa dsa = new dsa();

            try
            {
                foreach (var child in item.Children)
                {
                    if (child.Value.StartsWith("(p"))
                    {
                        dsa.p = child.Value.Split(' ')[1].TrimEnd(')').Trim('#');
                    }
                    else if (child.Value.StartsWith("(q"))
                    {
                        dsa.q = child.Value.Split(' ')[1].TrimEnd(')').Trim('#');
                    }
                    else if (child.Value.StartsWith("(g"))
                    {
                        dsa.g = child.Value.Split(' ')[1].TrimEnd(')').Trim('#');
                    }
                    else if (child.Value.StartsWith("(y"))
                    {
                        dsa.y = child.Value.Split(' ')[1].TrimEnd(')').Trim('#');
                    }
                    else if (child.Value.StartsWith("(x"))
                    {
                        dsa.x = child.Value.Split(' ')[1].TrimEnd(')').Trim('#');
                    }
                }
            }
            catch
            {
                throw new Exception("Parse exception");
            }

            return dsa;
        }
        #endregion
    }
}