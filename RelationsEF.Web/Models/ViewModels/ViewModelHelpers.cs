using RelationsEF.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelationsEF.Web.Models.ViewModels
{
    public static class ViewModelHelpers
    {
        public static UserProfileViewModel ToViewModel(this UserProfile userProfile)
        {
            var userProfileViewModel = new UserProfileViewModel
            {
                Name = userProfile.Name,
                UserProfileID = userProfile.UserProfileID
            };

            return userProfileViewModel;
        }

        public static UserProfile ToDomainModel(this UserProfileViewModel userProfileViewModel)
        {
            return new UserProfile
            {
                UserProfileID = userProfileViewModel.UserProfileID,
                Name = userProfileViewModel.Name
            };
        }
    }
}