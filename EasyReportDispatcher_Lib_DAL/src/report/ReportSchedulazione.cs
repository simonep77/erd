﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdo.Attributes;
using Bdo.Objects;

namespace EasyReportDispatcher_Lib_DAL.src.report
{
    [Table("report_piano_schedulazione")]
    public abstract class ReportSchedulazione : DataObject<ReportSchedulazione>
    {
        public const string KEY_TRIGGER = @"KEY_TRIGGER";
        
        [PrimaryKey, AutoIncrement()]
        public abstract long Id { get; }

        public abstract int EstrazioneId { get; set; }
        
        [PropertyMap(nameof(EstrazioneId))]
        public abstract ReportEstrazione Estrazione { get; }

        [MaxLength(50),SearchKey(KEY_TRIGGER)]
        public abstract string TriggerKey { get; set; }

        public abstract DateTime DataEsecuzione { get; set; }

        public abstract sbyte StatoId { get; set; }

        [PropertyMap(nameof(StatoId))]
        public abstract ReportSchedulazioneStato Stato { get; }

        [AcceptNull]
        public abstract long OutputId { get; set; }
        
        [PropertyMap(nameof(OutputId))]
        public abstract ReportEstrazioneOutput Output { get; }


        [AutoInsertTimestamp]
        public abstract DateTime DataInserimento { get; }

        [AutoUpdateTimestamp]
        public abstract DateTime DataAggiornamento { get; }

    }
}
