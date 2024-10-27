<template>
  <div>
    <h2>Weather Forecast</h2>
    {{weatherForecast}}
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "WeatherForecast",
  data() {
    return {
      weatherForecast: null
    }
  },
  mounted() {
    const token = localStorage.getItem("token");
    axios.defaults.headers.common["Authorization"] = "Bearer " + token;
    this.getWeatherForecast();
  },
  methods: {
    async getWeatherForecast() {
      try {
        const response = await axios.get("http://localhost:5206/WeatherForecast")
            .then(response => {
              console.log(response.data);
              this.weatherForecast = response.data;
            })
            .catch(error => {
              console.log(error.response.status);
              this.weatherForecast = "401 Unauthorized!";
            })
      } catch (error) {
        console.log(error);
      }
    }
  }
}
</script>