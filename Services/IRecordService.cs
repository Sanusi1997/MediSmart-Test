using MediSmart.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediSmart.API.Services
{
    public interface IRecordService
    {
        Task<Record> AddPatientRecord(Record patientRecord);
        Record UpdatePatientRecord(Record patientRecord);
        Task<Record> GetPatientRecordById(int patientId);
        Task<IEnumerable<Record>> GetPatientRecords();
        void DeletePatientRecord(int patientId);

    }
}
