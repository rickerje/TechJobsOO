using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view

            Job thisJob = new Job();

            thisJob = jobData.Find(id);


            return View(thisJob);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            if(ModelState.IsValid)
            {
                Job thisJob = new Job();
                thisJob.Name = newJobViewModel.Name;
                thisJob.Employer = jobData.Employers.Find(newJobViewModel.EmployerID);
                thisJob.Location = jobData.Locations.Find(newJobViewModel.LocationID);
                thisJob.CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID);
                thisJob.PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID);

                jobData.Jobs.Add(thisJob);
                return Redirect("/Job?id=" + thisJob.ID);
            }


            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

            return View(newJobViewModel);
        }
    }
}
