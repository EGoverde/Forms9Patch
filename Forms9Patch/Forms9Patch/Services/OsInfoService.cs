﻿using System;

namespace Forms9Patch
{
    /// <summary>
    /// Provides information about the operating system in which the app is running
    /// </summary>
    public static class OsInfoService
    {
        static OsInfoService()
        {
            Settings.ConfirmInitialization();
        }

        static IOsInformationService _service;
        static IOsInformationService Service
        {
            get
            {
                _service = _service ?? Xamarin.Forms.DependencyService.Get<IOsInformationService>();
                if (_service == null)
                    throw new ServiceNotAvailableException("OsInfoService not available");
                return _service;
            }
        }

        /// <summary>
        /// Operating system version
        /// </summary>
        public static Version Version => Service.Version;

    }
}
