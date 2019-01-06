using AutoMapper;

namespace CodeGenHero.ResourceScheduler.Repository.Mappers
{
	public class AutoMapperInitializer
	{
		public static void Initialize()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.AddProfiles(typeof(AutoMapperInitializer));
			});
		}
	}
}