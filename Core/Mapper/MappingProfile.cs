using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Dto;
using Core.Models;
using AutoMapper;

namespace Core.Mapper {
	/// <summary>
	/// Мап моделей и ДТО
	/// </summary>
	public class MappingProfile: Profile {
		public MappingProfile() {
			CreateMap<NewUserDto, User>()
				.ForMember(modelUser => modelUser.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

			CreateMap<User, UserDto>().ReverseMap();
		}
	}
}
