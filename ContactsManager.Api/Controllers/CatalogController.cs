using ContactsManager.Api.Data;
using ContactsManager.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class CatalogController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public CatalogController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("contact-form")]
    public async Task<ActionResult<ContactFormOptionsDTO>> GetContactFormOptionsAsync(CancellationToken cancellationToken)
    {
        var result = new ContactFormOptionsDTO
        {
            Categories = await _dbContext.Categories
                .AsNoTracking()
                .OrderBy(category => category.Name)
                .Select(category => new CategoryOptionDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    SubcategoryMode = category.SubcategoryMode
                })
                .ToListAsync(cancellationToken),
            Subcategories = await _dbContext.Subcategories
                .AsNoTracking()
                .OrderBy(subcategory => subcategory.Name)
                .Select(subcategory => new LookupOptionDTO
                {
                    Id = subcategory.Id,
                    Name = subcategory.Name
                })
                .ToListAsync(cancellationToken),
            Educations = await _dbContext.Educations
                .AsNoTracking()
                .OrderBy(education => education.Name)
                .Select(education => new LookupOptionDTO
                {
                    Id = education.Id,
                    Name = education.Name
                })
                .ToListAsync(cancellationToken)
        };

        return Ok(result);
    }
}
