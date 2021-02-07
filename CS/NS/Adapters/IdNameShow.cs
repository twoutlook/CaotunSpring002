
using System;
using System.Collections.Generic;



namespace NS.Adapter
{
    public partial class IdNameShow:IdName
    {
        public IdNameShow(int id, string name,string show)
        {
            Id = id;
            Name = name;
            Show = show;
        }
        public string Show { get; set; }

    }
}