//.NET
global using System;
global using Microsoft.Extensions.Options;
global using System.IdentityModel.Tokens.Jwt;
global using Microsoft.IdentityModel.Tokens;
global using System.Security.Claims;
global using Microsoft.AspNetCore.Authorization;

//nuget
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;
global using MongoDB.Driver;
global using FluentValidation;
global using FluentValidation.AspNetCore;

//Customer
global using Meals.Models;
global using Meals.Context;
global using Meals.Configuration;
global using Meals.Repositories;
global using Meals.MealService;
global using Meals.GraphQL.Queries;
global using Meals.Validators;
global using Meals.API.Keysettings;