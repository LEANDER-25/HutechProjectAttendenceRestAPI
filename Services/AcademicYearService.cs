using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RESTAPIRNSQLServer.DBContext;
using RESTAPIRNSQLServer.DTOs.AcademicYearDTOs;
using RESTAPIRNSQLServer.IServices;

namespace RESTAPIRNSQLServer.Services
{
    public class AcademicYearService : IAcademicYearService
    {
        private readonly AttendenceDBContext _context;
        private readonly IMapper _mapper;

        public AcademicYearService(AttendenceDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AcademicYearReadDTO>> GetAllYears()
        {
            var academicYears = await _context.AcademicYears.ToListAsync();
            var readDto = _mapper.Map<IEnumerable<AcademicYearReadDTO>>(academicYears);
            return readDto;
        }

        public async Task<AcademicYearReadDTO> GetSpecifyYears(string yearName)
        {
            var academicYear = await _context.AcademicYears.Where(year => year.AcademicYearName.Equals(yearName)).FirstOrDefaultAsync();
            return _mapper.Map<AcademicYearReadDTO>(academicYear);
        }
    }
}