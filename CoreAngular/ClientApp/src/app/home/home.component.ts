import { Component } from '@angular/core';
import { ODataClient, ODataServiceFactory } from "angular-odata";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(private factory: ODataServiceFactory) {
    this.select();
  }

  private select() {
    let airportsService = this.factory.entitySet<any>(
      "Airports",
      "Microsoft.OData.SampleService.Models.TripPin.Airport"
    );
    let airports = airportsService.entities();

    // Fetch airports
    airports.fetch().subscribe(({ entities }) => {
      console.log("Airports: ", entities);
    });



    //let forecastService = this.factory.entitySet<WeatherForecast>(
    //  "",
    //  ""
    //);

    //let forecasts = forecastService.entities();

    //// Fetch airports
    //forecasts.fetch().subscribe(({ entities }) => {
    //  console.log("Airports: ", entities);
    //});
  }

}


export class WeatherForecast {
  date!: Date;
  temperatureC!: number;
  temperatureF!: number;
  summary!: string;
}
