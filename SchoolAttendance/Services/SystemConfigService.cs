using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchoolAttendance.Services
{
    public class SystemConfigService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SystemConfigService> _logger;

        public SystemConfigService(IUnitOfWork unitOfWork, ILogger<SystemConfigService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<AppResponse<SystemConfig>> MacAddressInit()
        {
            try
            {
                var macAddress =  MacAddressFinder.GetAddress();
                _logger.LogInformation($"MacAddress fetched successfully: {macAddress}");

                var sysconfig = await _unitOfWork.SystemConfigs.
                    GetByIdAsync(x=> x.MacAddress == macAddress);

                if (sysconfig == null || sysconfig.MacAddress != macAddress)
                {
                    if (sysconfig != null)
                    {
                        _logger.LogInformation($"Updating macAddress from {sysconfig.MacAddress} to {macAddress}");
                        await _unitOfWork.SystemConfigs.DeleteAsync(sysconfig);
                    }

                    await _unitOfWork.SystemConfigs.
                        CreateAsync(new SystemConfig { MacAddress = macAddress });

                    _logger.LogInformation($"New MacAddress has been set: {macAddress}");
                }

                string response = JsonSerializer.Serialize(sysconfig,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                return new AppResponse<SystemConfig>(sysconfig);
            }
            catch (UnauthorizedAccessException ex)
            {
                string error = $"Permission error: {ExceptionHelper.GetExceptionDetails(ex)}";
                _logger.LogError(error);
                return new AppResponse<SystemConfig>(new List<string> { error }, false);
            }
            catch (PlatformNotSupportedException ex)
            {
                string error = $"Platform not supported: {ExceptionHelper.GetExceptionDetails(ex)}";
                _logger.LogError(error);
                return new AppResponse<SystemConfig>(new List<string> { error }, false);
            }
            catch (Exception ex)
            {
                string error = $"An error occurred in MacAddressInit: {ExceptionHelper.GetExceptionDetails(ex)}";
                _logger.LogError(error);
                return new AppResponse<SystemConfig>(new List<string> { error }, false);
            }
        }
    }
}