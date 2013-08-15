namespace OffTheRecord.Model.Pidgin
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    internal class Item
    {
        public Item()
        {
            this.Children = new Collection<Item>();
        }

        public Item Parent { get; set; }
        public Collection<Item> Children { get; set; }
        public string Value { get; set; }

        public bool IsRoot
        {
            get
            {
                return this.Parent == null;
            }
        }
    }
}
