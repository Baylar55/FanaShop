﻿using Web.ViewModels.Contact;

namespace Web.Services.Abstract
{
    public interface IContactService
    {
        Task<ContactIndexVM> GetAsync();
    }
}
