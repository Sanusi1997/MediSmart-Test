using MediSmart.API.Data;
using MediSmart.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediSmart.API.Services
{
    public class RecordService : IRecordService
    {
        private readonly RecordDbContext _context;
        public RecordService(RecordDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));

        }
        public async Task<Record> AddPatientRecord(Record patientRecord)
        {
            if (patientRecord == null)
            {
                throw new ArgumentNullException(nameof(patientRecord));
            }
            try
            { 
                await _context.Records.AddAsync(patientRecord);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return patientRecord;
        }

        public void DeletePatientRecord(int patientId)
        {
            var patientRecord = _context.Records.Where(r => r.PatientId == patientId).FirstOrDefault();
            _context.Records.Remove(patientRecord);
            _context.SaveChanges();
        }

        public async Task<Record> GetPatientRecordById(int patientId)
        {
            return await _context.Records.Where(r => r.PatientId == patientId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Record>> GetPatientRecords()
        {
            try
            {
                return await _context.Records.OrderBy(r => r.PatientId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Record UpdatePatientRecord(Record patientRecord)
        {
            if (patientRecord == null)
            {
                throw new ArgumentNullException(nameof(patientRecord));
            }
            try
            {
                var updatedEntity = _context.Records.Attach(patientRecord);
                updatedEntity.State = EntityState.Modified;
                _context.SaveChanges();
                return patientRecord;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
