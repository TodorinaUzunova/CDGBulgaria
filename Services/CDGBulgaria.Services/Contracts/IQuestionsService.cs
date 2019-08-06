using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDGBulgaria.Services.Contracts
{
	public interface IQuestionsService
	{
		IQueryable<QuestionServiceModel> GetAllQuestions();

		Task<bool> Create(QuestionServiceModel serviceModel);
	}
}
