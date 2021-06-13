/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;

namespace Smartflow.Abstraction.DTO
{
	public class PendingDto
	{
		public virtual string NID
		{
			get;
			set;
		}

		public virtual string ActorID
		{
			get;
			set;
		}

		public virtual string NodeID
		{
			get;
			set;
		}

		public virtual string InstanceID
		{
			get;
			set;
		}

		public virtual string NodeName
		{
			get;
			set;
		}

		public virtual string CategoryCode
		{
			get;
			set;
		}

		public virtual string CategoryName
		{
			get;
			set;
		}

		public virtual string Url
		{
			get;
			set;
		}

		public virtual DateTime CreateTime
		{
			get;
			set;
		}
	}
}
