// Simple Permissions Manager (https://github.com/raste/PermissionsManager)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using System.Security.Cryptography;

namespace BusinessLayer
{
    public class BUsers
    {

        object addingUserSync = new object();

        public void AddUser(Entities objectContext, string username, string name, string password, bool isAdmin)
        {
            Tools.CheckObjectContext(objectContext);

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("username is null or empty");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name is null or empty");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("password is null or empty");
            }

            lock (addingUserSync)
            {
                if (GetUser(objectContext, username, false) != null)
                {
                    throw new InvalidOperationException("There is already user with same username");
                }

                bool isFirstAdmin = false;
                User firstUser = objectContext.UserSet.FirstOrDefault();
                if (firstUser == null)
                {
                    isFirstAdmin = true;
                }

                User newUser = new User();

                newUser.username = username;
                newUser.name = name;
                newUser.isAdmin = isAdmin;
                newUser.password = GetHashed(password);
                newUser.isActive = true;
                newUser.mainAdmin = isFirstAdmin;

                objectContext.AddToUserSet(newUser);
                Tools.Save(objectContext);

            }

        }

        public User GetUser(Entities objectContext, string username, bool throwExcIfNull)
        {
            Tools.CheckObjectContext(objectContext);

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("username is null or empty");
            }

            User user = objectContext.UserSet.FirstOrDefault(usr => usr.username == username);

            if (user == null && throwExcIfNull == true)
            {
                throw new ArgumentOutOfRangeException(string.Format("No user with username = {0}", username));
            }

            return user;
        }

        public User GetUser(Entities objectContext, long id, bool throwExcIfNull)
        {
            Tools.CheckObjectContext(objectContext);

            if (id < 1)
            {
                throw new ArgumentOutOfRangeException("id < 1");
            }

            User user = objectContext.UserSet.FirstOrDefault(usr => usr.ID == id);

            if (user == null && throwExcIfNull == true)
            {
                throw new ArgumentOutOfRangeException(string.Format("No user with id = {0}", id));
            }

            return user;
        }

        public User GetUser(Entities objectContext, string username, string password, bool throwExcIfNull)
        {
            Tools.CheckObjectContext(objectContext);

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("username is null or empty");
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("password is null or empty");
            }

            string hashedPass = GetHashed(password);

            User user = objectContext.UserSet.FirstOrDefault(usr => usr.username == username && usr.password == hashedPass);

            if (user == null && throwExcIfNull == true)
            {
                throw new ArgumentOutOfRangeException(string.Format("No user with username = {0} and pass = ***", username));
            }

            return user;
        }

        public List<User> GetAllNonAdminUsers(Entities objectContext)
        {
            Tools.CheckObjectContext(objectContext);

            List<User> users = objectContext.UserSet.Where(usr => usr.isAdmin == false).ToList();

            return users;
        }

        /// <summary>
        /// Counts non admin users
        /// </summary>
        /// <param name="objectContext"></param>
        /// <returns></returns>
        public int CountUsers(Entities objectContext)
        {
            Tools.CheckObjectContext(objectContext);

            int result = 0;

            result = objectContext.UserSet.Count(usr => usr.isAdmin == false);

            return result;
        }

        private string GetHashed(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("password is null or empty");
            }

            UTF8Encoding encoder = new UTF8Encoding();
            SHA256CryptoServiceProvider sha256hasher = new SHA256CryptoServiceProvider();
            byte[] hashed256bytes = sha256hasher.ComputeHash(encoder.GetBytes(password));

            StringBuilder output = new StringBuilder();
            for (int i = 0; i < hashed256bytes.Length; i++)
            {
                output.Append(hashed256bytes[i].ToString("X2"));
            }

            return output.ToString();
        }

        public bool IsUserAdmin(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user is null or empty");
            }

            return user.isAdmin;
        }

        public void ActivateUser(Entities objectContext, User user)
        {
            Tools.CheckObjectContext(objectContext);
            if (user == null)
            {
                throw new ArgumentNullException("user is null or empty");
            }

            if (user.isActive == false)
            {
                user.isActive = true;
                Tools.Save(objectContext);
            }
        }

        public void DeactivateUser(Entities objectContext, User user)
        {
            Tools.CheckObjectContext(objectContext);
            if (user == null)
            {
                throw new ArgumentNullException("user is null or empty");
            }

            if (user.isActive == true)
            {
                user.isActive = false;
                Tools.Save(objectContext);
            }
        }

        public int RegisteredUsersCount(Entities objectContext)
        {
            Tools.CheckObjectContext(objectContext);

            int count = objectContext.UserSet.Count();

            return count;
        }

        public List<User> SearchForUser(Entities objectContext, string name)
        {
            Tools.CheckObjectContext(objectContext);
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name is null or empty");
            }

            // by default it must be done with stored procedure

            List<User> users = objectContext.UserSet.ToList();
            List<User> results = new List<User>();

            if (users != null && users.Count > 0)
            {
                foreach (User user in users)
                {
                    if (user.name.ToLower().Contains(name) == true)
                    {
                        results.Add(user);
                    }
                }

            }

            return results;
        }

    }
}
