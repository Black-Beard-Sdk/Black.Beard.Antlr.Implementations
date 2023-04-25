﻿namespace Bb.SqlServer.Client
{
    public class ConnectionStringSettings : List<ConnectionStringSetting>
    {

        public ConnectionStringSetting? GetConnectionString(string name)
        {
            return this.FirstOrDefault(x => x.Name == name);
        }


    }
}
