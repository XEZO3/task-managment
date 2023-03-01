using AutoMapper;
using Domain.IRepository;
using Domain.IService;
using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.ServiceRespone;
using Domain.utility;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace EC.Service.Service
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _user;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private ServiceRespone<UsersRespone> UserObj { get; set; }
        private ServiceRespone<IEnumerable<UsersRespone>> UserList { get; set; }
        public UserService(IUserRepository user, IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
        {
            _user = user;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
            UserObj = new ServiceRespone<UsersRespone>() { returnCode = Convert.ToString(codes.ok) };
            UserList = new ServiceRespone<IEnumerable<UsersRespone>>() { returnCode = Convert.ToString(codes.ok) };
        }

        public async Task<ServiceRespone<UsersRespone>> Add(Users entity)
        {
           
            var user = await _user.Add(entity);
            _unitOfWork.Save();
            UserObj.result =_mapper.Map<UsersRespone>(user);
           
            return UserObj;
        }
        public async Task<ServiceRespone<UsersRespone>> DeleteById(int Id)
        {
            var user = await _user.GetById(Id);
            _user.Delete(user);
            _unitOfWork.Save();
            return UserObj;
        }
        public ServiceRespone<UsersRespone> Delete(Users entity)
        {
             _user.Delete(entity);
            _unitOfWork.Save();
            return UserObj;
        }

        public async Task<ServiceRespone<UsersRespone>> FirstOrDefult(Expression<Func<Users, bool>> predicate = null)
        {
            var user = await _user.FirstOrDefult(predicate);  
            UserObj.result = _mapper.Map<UsersRespone>(user);
            return UserObj;
        }

        

        public async Task<ServiceRespone<IEnumerable<UsersRespone>>> GetAll(Expression<Func<Users, bool>> predicate = null)
        {
           var Users = await _user.GetAll(predicate);
            UserList.result = _mapper.Map<IEnumerable<UsersRespone>>(Users);
            return UserList;
        }

        public async Task<ServiceRespone<UsersRespone>> GetById(int Id)
        {
            var user = await _user.GetById(Id);
            UserObj.result = _mapper.Map<UsersRespone>(user);
            return UserObj;
        }

        public async Task<ServiceRespone<LoginRespone>> Login(LoginDto login)
        {
            ServiceRespone<LoginRespone> respone = new ServiceRespone<LoginRespone>();
            respone.result = new LoginRespone();
            var user = _user.GetByEmail(login.Email);
            if (user == null)
            {
                respone.returnCode = Convert.ToString(codes.ok);
                respone.errorMsg = "Email or password is incorrect";
                
            }
            else if (user.Password == GeneratePassword(login.Password, user.salt))
            {
                respone.returnCode = Convert.ToString(codes.ok);
                respone.result.Token = _tokenService.GenerateToken(user);
               
            }
            else {
                respone.returnCode = Convert.ToString(codes.ok);
                respone.errorMsg = "Email or password is incorrect";
            }
            return respone;
           
        }

        public async Task<ServiceRespone<UsersRespone>> Register(RegisterDto register)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            
            var user = _user.GetByEmail(register.Email);
            if (user != null)
            {
                UserObj.errorMsg = "Email is already taken";
                
            }
            else { 
                Users mapedUser = _mapper.Map<Users>(register);
                mapedUser.Password = GeneratePassword(register.Password,salt);
                mapedUser.salt= salt;
                await _user.Add(mapedUser);
                _unitOfWork.Save();               
                UserObj.result = _mapper.Map<UsersRespone>(mapedUser);
            }
            return UserObj;
        }

        public  ServiceRespone<UsersRespone> Update(Users entity)
        {
            if (entity.Password != "")
            {
                byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
                var password = entity.Password;
                var passwordhashed = GeneratePassword(password, salt);
                entity.Password = passwordhashed;
                entity.salt = salt;
            }
              
            var user =  _user.Update(entity);
            _unitOfWork.Save();
            UserObj.result = _mapper.Map<UsersRespone>(user);

            return UserObj;
        }
        
        public string GeneratePassword(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
            return hashed;
        }

        
    }
}
