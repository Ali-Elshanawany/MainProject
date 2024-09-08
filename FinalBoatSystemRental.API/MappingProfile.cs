using AutoMapper;
using FinalBoatSystemRental.Application.Boat.ViewModels;
using FinalBoatSystemRental.Core.Entities;
using FinalBoatSystemRental.Core.ViewModels.Addition;
using FinalBoatSystemRental.Core.ViewModels.Boat;
using FinalBoatSystemRental.Core.ViewModels.BoatBooking;
using FinalBoatSystemRental.Core.ViewModels.Cancellation;
using FinalBoatSystemRental.Core.ViewModels.Customer;
using FinalBoatSystemRental.Core.ViewModels.Owner;
using FinalBoatSystemRental.Core.ViewModels.Reservation;
using FinalBoatSystemRental.Core.ViewModels.Trip;

namespace FinalBoatSystemRental.API;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        // Boat Mapping
        CreateMap<Boat, AddBoatViewModel>().ReverseMap();
        CreateMap<Boat, UpdateBoatViewModel>().ReverseMap();
        CreateMap<Boat, BoatViewModel>().ReverseMap();
        CreateMap<IEnumerable<Boat>, UpdateBoatViewModel>().ReverseMap();


        /// Trip Mapping

        CreateMap<Trip, TripViewModel>().ReverseMap();
        CreateMap<Trip, AvailableTripsViewModel>().ReverseMap();
        //CreateMap<CustomAvailableTripsViewModel, AvailableTripsViewModel>();

        /// Addition Mapping
        CreateMap<Addition, AdditionViewModel>().ReverseMap();

        // Owner Mapping

        CreateMap<Owner, OwnerViewModel>().ReverseMap();


        // Customer Mapping

        CreateMap<Customer, CustomerViewModel>().ReverseMap();


        // boat booking

        CreateMap<BoatBooking, BoatBookingViewModel>().ReverseMap();
        CreateMap<BoatBooking, BoatBookingHistoryViewModel>().ReverseMap();
        CreateMap<BoatBooking, ListBoatBookingOwner>().ReverseMap();

        // Reservation 

        CreateMap<Reservation, ReservationViewModel>().ReverseMap();

        // Cancellation
        CreateMap<Cancellation, CancellationViewModel>().ReverseMap();



    }
}
