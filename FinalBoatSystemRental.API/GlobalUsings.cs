
global using FinalBoatSystemRental.API.Exceptions;
// Microsoft 
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
// System
global using System.Reflection;
global using System.Text;
global using System.Net;
global using System.Security.Claims;
// Packages
global using Serilog;
global using MediatR;
global using AutoMapper;
global using FluentValidation;
// Application
global using FinalBoatSystemRental.Application;
global using FinalBoatSystemRental.Application.Trip.Query.Get;
global using FinalBoatSystemRental.Application.Addition.Command.Add;
global using FinalBoatSystemRental.Application.Trip.Query.List;
global using FinalBoatSystemRental.Application.Boat.Command.Delete;
global using FinalBoatSystemRental.Application.Boat.Query.Get;
global using FinalBoatSystemRental.Application.Boat.Query.List;
global using FinalBoatSystemRental.Application.Owner.Query.GetDetails;
global using FinalBoatSystemRental.Application.Reservation.Command;
global using FinalBoatSystemRental.Application.Reservation.Query.List;
global using FinalBoatSystemRental.Application.BoatBooking.Command.Add;
global using FinalBoatSystemRental.Application.BoatBooking.Query.List;
global using FinalBoatSystemRental.Application.Boat.Command.Add;
global using FinalBoatSystemRental.Application.Owner.Command.Add;
global using FinalBoatSystemRental.Application.Owner.Command.Update;
global using FinalBoatSystemRental.Application.Services;
global using FinalBoatSystemRental.Application.Trip.Command.Add;
global using FinalBoatSystemRental.Application.Cancellation.Command.Add;
global using FinalBoatSystemRental.Application.Customer.Command.Update;
global using FinalBoatSystemRental.Application.Customer.Query.GetDetails;
global using FinalBoatSystemRental.Application.Boat.Command.Update;

// Core 
global using FinalBoatSystemRental.Core;
global using FinalBoatSystemRental.Core.Entities;
global using FinalBoatSystemRental.Core.Interfaces;
// Infrastructure 
global using FinalBoatSystemRental.Infrastructure;
global using FinalBoatSystemRental.Infrastructure.Repositories;


