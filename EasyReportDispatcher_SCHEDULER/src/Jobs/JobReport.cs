﻿using Bdo.Objects;
using EasyReportDispatcher_Lib_BIZ.src.report;
using EasyReportDispatcher_Lib_Common.src.enums;
using EasyReportDispatcher_Lib_DAL.src.query;
using EasyReportDispatcher_Lib_DAL.src.report;
using EasyReportDispatcher_SCHEDULER.src.Common;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReportDispatcher_SCHEDULER.src.Jobs
{
    class JobReport : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => {

                {
                    var ret = new ErdJobResult();
                    context.Result = ret;

                    var repDefId = (int)context.JobDetail.JobDataMap[CostantiSched.JobDataMap.Reports.ReportId];
                    var repName = context.JobDetail.JobDataMap[CostantiSched.JobDataMap.Reports.ReportName].ToString();
                    var bSendEmail = false;
                    var sb = new StringBuilder();


                    using (var slot = AppContextERD.Service.CreateSlot())
                    {
                        //Scrive nel log il debug User1
                        slot.OnLogDebugSent += ((a, b, c) => { if (b == DebugLevel.User_1) sb.AppendLine(c); });

                        ReportEstrazioneBIZ repBiz = slot.BizNewWithLoadByPK<ReportEstrazioneBIZ>(repDefId);

                        repBiz.Run(true, bSendEmail, true);

                        ret.IsOK = (repBiz.LastResult.StatoId == eReport.StatoEstrazione.TerminataConSuccesso && string.IsNullOrWhiteSpace(repBiz.LastResult.MailEsito));
                        ret.Message = $"{repBiz.LastResult.EstrazioneEsito} {repBiz.LastResult.MailEsito}".Trim();
                        ret.Output = repBiz.LastResult;

                    }


                }

            });
        }


    }
}
