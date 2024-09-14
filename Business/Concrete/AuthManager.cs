using Business.Abstract;
using Business.Constants;
using Core.Entities.Concretes;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entity.Concrete;
using Entity.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;
        ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateToken(user,claims);
            return new SuccessDataResult<AccessToken>(accessToken,Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDTO userForLoginDTO)
        {
            var userCheck = _userService.GetByEmail(userForLoginDTO.Email);
            if (userCheck.Data==null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            var passwordCheck = HashingHelper.VerifyPassword(userForLoginDTO.Password,userCheck.Data.PasswordHash,
                userCheck.Data.PasswordSalt);
            if (!passwordCheck)
            {
                return new ErrorDataResult<User>(Messages.InvalidPassword);
            }
            return new SuccessDataResult<User>(userCheck.Data,Messages.LoginSuccess);
        }

        public IDataResult<User> Register(UserForRegisterDTO userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(userForRegisterDto.Password,
                out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var result = _userService.Add(user);
            if (result.IsSuccess)
            {
                return new SuccessDataResult<User>(user, Messages.UserRegistered);
            }
            return new ErrorDataResult<User>(user,result.Message);
        }

        public IResult UserExists(string email)
        {
            var result = _userService.GetByEmail(email);
            if (result.Data!=null)
            {
                return new ErrorResult(Messages.EmailExists);
            }
            return new SuccessResult();
        }
    }
}
