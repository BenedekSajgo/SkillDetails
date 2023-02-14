public static class MyServices
{
	public static void AddMyServices(this IServiceCollection services)
	{
		services.AddScoped<IPersonRepository, PersonRepository>();
		services.AddScoped<PersonManager>();
		services.AddScoped<EducationManager>();
		services.AddScoped<IProjectRepository, ProjectRepository>();
		services.AddScoped<ProjectManager>();
		services.AddScoped<ICategoryRepository, CategoryRepository>();
		services.AddScoped<CategoryManager>();
		services.AddScoped<ISkillRepository, SkillRepository>();
		services.AddScoped<SkillManager>();
		services.AddScoped<SkillLevelManager>();
		services.AddScoped<IPersonValidator, PersonValidator>();
		services.AddScoped<IEducationValidator, EducationValidator>();
		services.AddScoped<IProjectValidator, ProjectValidator>();
		services.AddScoped<ISkillValidator, SkillValidator>();
		services.AddScoped<ISkillLevelValidator, SkillLevelValidator>();
		services.AddScoped<ISkillCategoryValidator, SkillCategoryValidator>();
	}
}