using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDGBulgaria.Services.Contracts
{
	public interface IAnswersService
	{
		IQueryable<AnswerServiceModel> GetAllAnswersForAQuestionById(string questionId);

		Task<bool> CreateAnswer(AnswerServiceModel serviceModel);
	}
}
