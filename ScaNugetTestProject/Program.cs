using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Serilog;
using Dapper;
using CsvHelper;
using RestSharp;
using Polly;
using AutoMapper;
using FluentValidation;
using Humanizer;
using Bogus;
using NodaTime;
using HtmlAgilityPack;
using Swashbuckle.AspNetCore;
using MediatR;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using System.Reactive.Linq;
using MailKit.Net.Smtp;
using MailKit;
using NuGet.Packaging;

namespace ScaNugetTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Newtonsoft.Json
            var json = JsonConvert.SerializeObject(new { Message = "Hello, World!" });
            Console.WriteLine($"Newtonsoft.Json: {json}");

            // Serilog (requires Serilog.Sinks.Console package)
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            Log.Information("Serilog: Logging to console");

            // Dapper (simulate usage)
            Console.WriteLine("Dapper: (Simulated) Dapper is referenced");

            // CsvHelper
            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.WriteRecord(new { Name = "CsvHelper" });
                csv.NextRecord();
                Console.WriteLine($"CsvHelper: {writer.ToString().Trim()}");
            }

            // RestSharp
            var client = new RestClient("https://example.com");
            var request = new RestRequest("/", Method.Get);
            Console.WriteLine($"RestSharp: Created request to {client.Options.BaseUrl}");

            // Polly
            var policy = Policy.Handle<Exception>().Retry(1);
            policy.Execute(() => Console.WriteLine("Polly: Retry policy executed"));

            // AutoMapper (Profile-based configuration for v15)
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new SimpleProfile()));
            var mapper = config.CreateMapper();
            var dest = mapper.Map<Dest>(new Source { Value = 42 });
            Console.WriteLine($"AutoMapper: {dest.Value}");

            // FluentValidation
            var validator = new InlineValidator<Source>();
            validator.RuleFor(x => x.Value).GreaterThan(0);
            var result = validator.Validate(new Source { Value = 1 });
            Console.WriteLine($"FluentValidation: {result.IsValid}");

            // Humanizer
            Console.WriteLine($"Humanizer: {5.ToWords()}");

            // Bogus
            var faker = new Faker();
            Console.WriteLine($"Bogus: {faker.Name.FullName()}");

            // NodaTime
            var now = SystemClock.Instance.GetCurrentInstant();
            Console.WriteLine($"NodaTime: {now}");

            // HtmlAgilityPack
            var doc = new HtmlDocument();
            doc.LoadHtml("<html><body><h1>HtmlAgilityPack</h1></body></html>");
            Console.WriteLine($"HtmlAgilityPack: {doc.DocumentNode.SelectSingleNode("//h1").InnerText}");

            // Swashbuckle.AspNetCore (simulate usage)
            Console.WriteLine("Swashbuckle.AspNetCore: Referenced");

            // MediatR (simulate usage)
            Console.WriteLine("MediatR: Referenced");

            // Xunit (simulate usage)
            Console.WriteLine("xUnit: Referenced");

            // Moq (simulate usage)
            var mock = new Mock<IDisposable>();
            Console.WriteLine($"Moq: Mock created {mock.Object != null}");

            // Microsoft.Extensions.Logging
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogInformation("Microsoft.Extensions.Logging: Logging info");

            // System.Reactive
            Observable.Range(1, 3).Subscribe(x => Console.WriteLine($"System.Reactive: {x}"));

            // MailKit (simulate usage)
            Console.WriteLine("MailKit: Referenced");

            // NuGet.Packaging (simulate usage)
            var package = new PackageBuilder();
            Console.WriteLine("NuGet.Packaging: PackageBuilder created");
        }

        public class Source { public int Value { get; set; } }
        public class Dest { public int Value { get; set; } }
        private class SimpleProfile : Profile
        {
            public SimpleProfile()
            {
                CreateMap<Source, Dest>();
            }
        }
    }
}
