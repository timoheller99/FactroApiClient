namespace FactroApiClient.Company.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FactroApiClient.Company.Contracts.Basic;
    using FactroApiClient.Company.Contracts.CompanyTag;

    /// <summary>
    /// Interface for the Company API
    /// </summary>
    public interface ICompanyApi
    {
        /// <summary>
        /// Creates a company.
        /// </summary>
        /// <param name="createCompanyRequest">Request model to create a new company.</param>
        /// <returns>Returns the created company.</returns>
        public Task<CreateCompanyResponse> CreateCompanyAsync(CreateCompanyRequest createCompanyRequest);

        /// <summary>
        /// Fetches a list of all companies visible to the requesting user.
        /// </summary>
        /// <returns>Returns the list of companies visible to the requesting user.</returns>
        public Task<IEnumerable<GetCompanyPayload>> GetCompaniesAsync();

        /// <summary>
        /// Fetches the company with the given company id.
        /// </summary>
        /// <param name="companyId">Id of the company to fetch.</param>
        /// <returns>Returns the company with the given <paramref name="companyId"/>.</returns>
        public Task<GetCompanyByIdResponse> GetCompanyByIdAsync(string companyId);

        /// <summary>
        /// Updates the company with the given company id.
        /// </summary>
        /// <param name="companyId">Id of the company to update.</param>
        /// <param name="updateCompanyRequest">Request model to update the company.</param>
        /// <returns>Returns the updated company.</returns>
        public Task<UpdateCompanyResponse> UpdateCompanyAsync(string companyId, UpdateCompanyRequest updateCompanyRequest);

        /// <summary>
        /// Deletes the company with the given company id.
        /// </summary>
        /// <param name="companyId">Id of the company to delete.</param>
        /// <returns>Returns the deleted company.</returns>
        public Task<DeleteCompanyResponse> DeleteCompanyAsync(string companyId);

        /// <summary>
        /// Creates an company tag.
        /// </summary>
        /// <param name="createCompanyTagRequest">Request model to create a company tag.</param>
        /// <returns>Returns the created company tag.</returns>
        public Task<CreateCompanyTagResponse> CreateCompanyTagAsync(CreateCompanyTagRequest createCompanyTagRequest);

        /// <summary>
        /// Fetches the company tags visible to the requesting user.
        /// </summary>
        /// <returns>Returns the list of company tags visible to the requesting user.</returns>
        public Task<IEnumerable<GetCompanyTagPayload>> GetCompanyTagsAsync();

        /// <summary>
        /// Fetches the company tags that are associated with the given company id.
        /// </summary>
        /// <param name="companyId">Id of the company whose tags should be fetched.</param>
        /// <returns>Returns the list of tags associated with the given company id.</returns>
        public Task<IEnumerable<GetCompanyTagPayload>> GetCompanyTagsAsync(string companyId);

        /// <summary>
        /// Deletes the company tag with the given company tag id.
        /// </summary>
        /// <param name="companyTagId">Id of the company tag to delete.</param>
        /// <returns>Returns the deleted company tag.</returns>
        public Task<DeleteCompanyTagResponse> DeleteCompanyTagAsync(string companyTagId);

        /// <summary>
        /// Creates an association between the given company id and the given company tag id.
        /// </summary>
        /// <param name="companyId">Id of the company to associate.</param>
        /// <param name="addCompanyTagAssociationRequest">Request model to create the association.</param>
        /// <returns>Returns void.</returns>
        public Task AddCompanyTagAsync(string companyId, AddCompanyTagAssociationRequest addCompanyTagAssociationRequest);

        /// <summary>
        /// Removes the association between the given company id and the given company tag id.
        /// </summary>
        /// <param name="companyId">Id of the company.</param>
        /// <param name="companyTagId">Id of the company tag.</param>
        /// <returns>Returns void.</returns>
        public Task RemoveCompanyTagAsync(string companyId, string companyTagId);
    }
}
