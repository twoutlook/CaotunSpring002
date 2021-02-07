using System;

namespace DreamAITek.T001.Adapter
{
    public interface IAdapter
    {
        IFiltersA000 GetFilter();
    //    bool IsLoading { get; set; }
    //    bool Loading { get; set; }
        string GetOrderBy();
        string GetSortString();
        string GetSortString2();
        string GetWhere();
        string GetWhereString();
        void Init(Type type, string PRE, string TABLE_CONFIG);
        void ReadJson(Type type, string PRE, string ENT);
        void Start(Type type, string PRE, string ENT, string TABLE_CONFIG);
        void UpdateFMapper(Type type);
        void WriteJson(string PRE, string ENT);
    }
}