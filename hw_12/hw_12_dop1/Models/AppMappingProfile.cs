using System.Reflection;
using AutoMapper;
using hw_12_dop1.ViewModel;

namespace hw_12_dop1.Models;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<User, UserViewModel>().ReverseMap();
    }
    
    public void Map<TTo, TFrom>(TTo classTo, TFrom classFrom)
    {
        PropertyInfo[] classFromProp = classFrom.GetType().GetProperties();
        PropertyInfo[] classToProp = classTo.GetType().GetProperties();
 
        foreach (PropertyInfo modelPropInfo in classFromProp)
        {
            foreach (PropertyInfo viewModelPropInfo in classToProp)
            {
                if (modelPropInfo.Name == viewModelPropInfo.Name && !modelPropInfo.GetGetMethod().IsVirtual && !viewModelPropInfo.GetGetMethod().IsVirtual)
                {
                    viewModelPropInfo.SetValue(classTo, modelPropInfo.GetValue(classFrom, null), null);
                    break;
                }
            }
        }
    }

}