using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Web.Controllers.Applicants;

namespace Hahn.ApplicatonProcess.December2020.Web.Utils
{
    //Define different kinds of model mapping here.
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Applicant, ApplicantResponse>();
        }
    }
}
