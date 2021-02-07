
using System;
using System.Collections.Generic;



namespace CaotunSpring.C000.Adapter
{
    public partial class A000FieldMapper
    {
        public A000FieldMapper() { }
        public A000FieldMapper(string id, string name)
        {
            Id = id;
            Name = name;
            Index = -1;
        }
        public string Id { get; set; }
        public string Name { get; set; }

        public int Index { get; set; }
        public string Ext { get; set; }

    }
}