using AutoMapper;
using MediSmart.API.Data.DTO;
using MediSmart.API.Entities;
using MediSmart.API.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediSmart.API.Controllers
{
    [EnableCors("SpecificOrigin")]
    [Route("api/records")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly ILogger<RecordsController> _logger;
        private readonly IRecordService _service;
        private readonly IMapper _mapper;
        public RecordsController(ILogger<RecordsController> logger, IRecordService service, IMapper mapper
            )
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _service = service ??
                throw new ArgumentNullException(nameof(service));

        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<RecordDTO>>> GetPatients()
        {
            try
            {

                var recordsFromRepo = await _service.GetPatientRecords();
                var recordsToDsiplay = _mapper.Map<IEnumerable<RecordDTO>>(recordsFromRepo);
                return Ok(recordsToDsiplay);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message.ToString());
                var exceptionResponse = new ResponseDTO(ex.Message.ToString(), false);
                return BadRequest(exceptionResponse);
            }
        }


        [HttpGet("{patientId}", Name = "GetPatientRecord")]
        public async Task<ActionResult<RecordDTO>> GetPatientRecord(int patientId)
        {
            try
            {

                var recordFromRepo = await _service.GetPatientRecordById(patientId);
                if (recordFromRepo == null)
                {

                    var notFoundResponse = new ResponseDTO("Patient record does not exist", false);
                    return NotFound(notFoundResponse);
                }
                var patientToReturn = _mapper.Map<RecordDTO>(recordFromRepo);
                return Ok(patientToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message.ToString());
                var exceptionResponse = new ResponseDTO(ex.Message.ToString(), false);
                return BadRequest(exceptionResponse);
            }

        }

        [HttpPost]
        public async Task<ActionResult<RecordDTO>> CreatePatientRecord(RecordCreateDTO record)
        {
            if (record == null)
            {
                var incorrectResponse = new ResponseDTO("One or more detail(s) incorrect", false);
                return BadRequest(incorrectResponse);
            }
            try
            {

                var recordEntity = _mapper.Map<Record>(record);
                recordEntity.CreatedOn = DateTime.UtcNow;
                recordEntity.Age = Convert.ToInt16(record.Age);
                await _service.AddPatientRecord(recordEntity);

                var recordToReturn = _mapper.Map<RecordDTO>(recordEntity);

                return CreatedAtRoute(

                    "GetPatientRecord", new { patientId = recordEntity.PatientId }, recordToReturn
                    );
            }


            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message.ToString());
                var exceptionResponse = new ResponseDTO(ex.Message.ToString(), false);
                return BadRequest(exceptionResponse);
            }
        }
        [HttpPut("{patientId}")]
        public async Task<ActionResult> UpdateDepartment(int patientId, RecordUpdateDTO updateDTO)
        {
            if (updateDTO == null)
            {
                var incorrectResponse = new ResponseDTO("One or more detail(s) incorrect", false);
                return BadRequest(incorrectResponse);
            }
            try
            {
                var recordInRepo = await _service.GetPatientRecordById(patientId);
                if (recordInRepo == null)
                {

                    var recordNotExistResponse = new ResponseDTO("Record does not exist", false);
                    return BadRequest(recordNotExistResponse);
                }

                _mapper.Map(updateDTO, recordInRepo);
                recordInRepo.Age = Convert.ToInt16(updateDTO.Age);
                recordInRepo.UpdatedOn = DateTime.UtcNow;
                _service.UpdatePatientRecord(recordInRepo);
                var successfulUpdateResponse = new ResponseDTO("Successfully updated record", true);
                return Ok(successfulUpdateResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message.ToString());
                var logResponse = new ResponseDTO(ex.Message, false);
                return BadRequest(logResponse);
            }
        }



        [HttpDelete("{patientId}")]
        public async Task<ActionResult> DeletePatientRecord(int patientId)
        {
            var recordInRepo = await _service.GetPatientRecordById(patientId);
            if (recordInRepo == null)
            {
                var recordNotExistResponse = new ResponseDTO("Patient record does not exist", false);
                return BadRequest(recordNotExistResponse);
            }
            _service.DeletePatientRecord(patientId);
            return NoContent();

        }
    }
}
