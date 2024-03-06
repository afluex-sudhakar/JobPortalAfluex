﻿using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;

namespace Data.Repositories
{
    public class StateRepository : RepositoryBase<State>, IStateRepository
    {
        public bool Add(StateDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var tt = new State();
                tt.CreatedAt = createdAt;
                tt.IsDeleted = false;
                tt.Name = req.Name;
                tt.NameH = req.NameH;
                Add(tt);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(UpdateStateDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var d = GetById(req.Id);
                if (d != null)
                {
                    d.ModifiedAt = createdAt;
                    d.Name = req.Name;
                    d.NameH = req.NameH;
                    Update(d);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id, int userId)
        {
            try
            {
                var d = GetById(id);
                if (d != null)
                {
                    d.IsDeleted = true;
                    d.ModifiedAt = new Constants().IST_DATE_TIME;
                    Update(d);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<State> GetAll()
        {
            try
            {
                return GetAll(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<State> GetDetailById(int Id)
        {
            try
            {
                return GetAll(x => x.Id == Id && x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}