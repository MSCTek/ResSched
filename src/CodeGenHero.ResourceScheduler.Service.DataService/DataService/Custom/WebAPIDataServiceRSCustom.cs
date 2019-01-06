using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeGenHero.Logging;
using CodeGenHero.DataService;
using CodeGenHero.ResourceScheduler.API.Client.Interface;
using CodeGenHero.ResourceScheduler.DTO.RS;

namespace CodeGenHero.ResourceScheduler.API.Client
{
	public partial class WebApiDataServiceRS : WebApiDataServiceBase, IWebApiDataServiceRS
	{

        /// Sample Implementation:
		//public async Task<IHttpCallResultCGHT<IPageDataT<List<Meeting>>>> GetMeetingsAndAttendeesByUserId(Guid userId, DateTime? minUpdatedDate, bool? isDeleted, string sort = null, int page = 1, int pageSize = 100)
		//{
		//	List<IFilterCriterion> filterCriteria = new List<IFilterCriterion>();

		//	IFilterCriterion filterCriterion = new FilterCriterion
		//	{
		//		FieldName = nameof(MeetingAttendee.UserId),
		//		FieldType = "Guid",
		//		FilterOperator = Constants.OPERATOR_ISEQUALTO,
		//		Value = userId
		//	};
		//	filterCriteria.Add(filterCriterion);

		//	if (minUpdatedDate.HasValue)
		//	{
		//		filterCriterion = new FilterCriterion
		//		{
		//			FieldName = nameof(Meeting.UpdatedDate),
		//			FieldType = "DateTime?",
		//			FilterOperator = Constants.OPERATOR_ISGREATERTHAN,
		//			Value = minUpdatedDate
		//		};
		//		filterCriteria.Add(filterCriterion);
		//	}

		//	if (minUpdatedDate.HasValue)
		//	{
		//		filterCriterion = new FilterCriterion
		//		{
		//			FieldName = nameof(Meeting.IsDeleted),
		//			FieldType = "bool?",
		//			FilterOperator = Constants.OPERATOR_ISEQUALTO,
		//			Value = isDeleted
		//		};
		//		filterCriteria.Add(filterCriterion);
		//	}

		//	IPageDataRequest pageDataRequest = new PageDataRequest(filterCriteria: filterCriteria, sort: sort, page: page, pageSize: pageSize);
		//	List<string> filter = BuildFilter(pageDataRequest.FilterCriteria);
		//	return await SerializationHelper.Instance.SerializeCallResultsGet<List<Meeting>>(Log, GetClient(),
		//		$"{ExecutionContext.BaseWebApiUrl}GetMeetingsAndAttendeesByUserId", filter, page: pageDataRequest.Page, pageSize: pageDataRequest.PageSize);
		//}

    }
}
