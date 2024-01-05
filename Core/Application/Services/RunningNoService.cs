//using Application.Interfaces;
//using Domain.Entities.Company;
//using Domain.Entities.DB;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Services
//{
//    public class RunningNoService : IRunningNoService
//    {
//        private readonly ICleanDbContext _context;

//        public RunningNoService(ICleanDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<string> PostRunning(int Id)
//        {
//            RunningCode Runing = _context.runningCodes.Where(x => x.Id == Id).FirstOrDefault();
//            string SoftName = Runing.SoftName;
//            int RunningNo = Runing.RunningNo + 1;
//            int digit = Runing.Digit;
//            string param2 = Runing.Param2;

//            //_context.runningCodes.Update(Runing);
//            Runing.RunningNo = RunningNo;

//            // Create a new entity to store the updated RunningNo
//            //RunningCode historyEntry = new RunningCode
//            //{
//            //    SoftName = SoftName,
//            //    RunningNo = RunningNo,

//            //};
            
//            await _context.SaveChangesAsync();
//            return SoftName + param2 + (RunningNo).ToString().PadLeft(digit, '0');
//        }
//    }
//}
