using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SBS.Infrastructure.Connection.Dapper
{
    public class BaseDapper
    {
        protected static IConfiguration _dapperConfiguration;

        public IConfiguration DapperConfiguration
        {
            get { return _dapperConfiguration; }
        }

        public BaseDapper(IConfiguration _configuration)
        {
            _dapperConfiguration = _configuration;
        }
    }
}
