using AutoMapper;
using BetsAreMade.DataContracts.Dbo.Users;
using BetsAreMade.DataContracts.Dto.Users;
using BetsAreMade.Repositories.NoSQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetsAreMade.Services
{
    public class UserService
    {
        private UserRepository _userRepository;

        private IMapper _mapper;

        public UserService(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateAsync(UserDto user)
        {
            return _mapper.Map<UserDto>(await _userRepository.CreateAsync(_mapper.Map<UserDbo>(user)));
        }

        public async Task<List<UserDto>> GetAsync()
        {
            return _mapper.Map<List<UserDto>>(await _userRepository.GetAsync());
        }

        public async Task<UserDto> GetByIdAsync(string id)
        {
            return _mapper.Map<UserDto>(await _userRepository.GetByIdAsync(id));
        }

        public async Task<UserDto> UpdateAsync(UserDto user)
        {
            return _mapper.Map<UserDto>(await _userRepository.UpdateAsync(_mapper.Map<UserDbo>(user)));
        }

        public async Task DeleteAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
