﻿using MediaStorage.Data.Entities;
using System;
using System.Threading.Tasks;

namespace MediaStorage.Data.Read
{
    public class UserWriteRepository
    {
        public bool AddUser(User user)
        {
            var result = false;
            using (var mediaContext = new MediaContext())
            {
                using (var transaction = mediaContext.Database.BeginTransaction())
                {
                    try
                    {
                        mediaContext.Users.Add(user);
                        mediaContext.SaveChanges();
                        result = true;
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            return result;
        }

        public bool DeleteUser(User user)
        {
            var result = false;
            using (var mediaContext = new MediaContext())
            {
                using (var transaction = mediaContext.Database.BeginTransaction())
                {
                    try
                    {
                        user.IsDeleted = true;
                        mediaContext.Users.Add(user);
                        mediaContext.SaveChanges();
                        result = true;
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            return result;
        }
    }
}
