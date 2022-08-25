using HappyDay.Domain;
using HappyDay.Models;
using HappyDay.Repositories;
using System.Net.Http.Headers;

namespace HappyDay.Services
{
    public class BirthdayService : IBirthdayService
    {
        private readonly IBirthdayRepository _birthdayRepository;
        private readonly IPictureService _pictureService;


        public BirthdayService(
            IBirthdayRepository birthdayRepository,
            IPictureService pictureService)
        {
            _birthdayRepository = birthdayRepository;
            _pictureService = pictureService;
        }

        public void Add(BirthdayModel model)
        {
            var id = _birthdayRepository.Add(new Birthday
            {
                UserId = model.UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BiDay = model.BiDay ?? DateTime.MinValue
            }).Id;

            if (model.Photo != null)
            {
                _pictureService.SaveByBirthdayId(model.Photo, id);
            }
        }

        public void DeleteById(string id)
        {
            _pictureService.DeleteByBirthdayId(id);
            _birthdayRepository.DeleteById(id);
        }

        public void Edit(BirthdayModel model)
        {
            if (model.Photo != null)
            {
                _pictureService.DeleteByBirthdayId(model.Id);
            }

            _birthdayRepository.Update(new Birthday
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BiDay = model.BiDay ?? DateTime.MinValue
            });
        }

        public BirthdayModel GetById(string id)
        {
            var birthday = _birthdayRepository.GetById(id);
            return new BirthdayModel
            {
                Id = birthday.Id,
                FirstName = birthday.FirstName,
                LastName = birthday.LastName,
                BiDay = birthday.BiDay,
                UserId = birthday.UserId,
                PhotoUrl = _pictureService.GetUrlByBirthdayId(birthday.Id)
            };
        }

        public BirthdaysModel GetByUserId(string UserId)
        {
            var birthdays = _birthdayRepository
                .GetByUserId(UserId)
                .OrderBy(b =>
                {
                    var diff = b.BiDay.DayOfYear - DateTime.Now.DayOfYear;
                    if(diff < 0)
                    {
                        diff += 365;
                    }
                    return diff;
                });

            if(birthdays == null)
            {
                return null;
            }

            var model = new BirthdaysModel();

            foreach(var birthday in birthdays)
            {
                var birthdayModel = new BirthdayModel
                {
                    Id = birthday.Id,
                    FirstName = birthday.FirstName,
                    LastName = birthday.LastName,
                    BiDay = birthday.BiDay,
                    UserId = birthday.UserId,
                    PhotoUrl = _pictureService.GetUrlByBirthdayId(birthday.Id)
                };
                var a = birthday.BiDay.Date;
                if ((birthday.BiDay.Month == DateTime.Now.Month)
                    && (birthday.BiDay.Day == DateTime.Now.Day))
                {
                    model.Current.Add(birthdayModel);
                }
                else
                {
                    model.Coming.Add(birthdayModel);
                }
            }

            return model;
        }
    }
}