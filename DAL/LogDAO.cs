﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DTO;

namespace DAL
{
    public class LogDAO : PostContext
    {
        public static void AddLog(int ProcessType, string TableName, int ProcessID)
        {
            try
            {
                Log_Table log = new Log_Table();
                log.UserID = UserStatic.UserID;
                log.ProcessType = ProcessType;
                log.ProcessID = ProcessID;
                log.ProcessCategoryType = TableName;
                log.ProcessDate = DateTime.Now;
                log.IPAddress = HttpContext.Current.Request.UserHostAddress;
                db.Log_Table.Add(log);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<LogDTO> GetLogs()
        {
            List<LogDTO> dtolist = new List<LogDTO>();
            var list = (from l in db.Log_Table
                        join u in db.T_User on l.UserID equals u.ID
                        join p in db.ProcessTypes on l.ProcessType equals p.ID 
                        select new
                        {
                            ID = l.ID,
                            UserName = u.Username,
                            TableName = l.ProcessCategoryType,
                            TableID = l.ProcessID,
                            ProcessName = p.ProcessName,
                            ProcessDate = l.ProcessDate,
                            ipAddress = l.IPAddress
                        }).OrderByDescending(x=>x.ProcessDate).ToList();
            foreach (var item in list)
            {
                LogDTO dto = new LogDTO();
                dto.ID = item.ID;
                dto.UserName = item.UserName;
                dto.ProcessDate = item.ProcessDate;
                dto.ProcessName = item.ProcessName;
                dto.TableID = item.TableID;
                dto.TableName = item.TableName;
                dto.IpAddress = item.ipAddress;
                dtolist.Add(dto);
            }
            return dtolist;
        }
    }
}
