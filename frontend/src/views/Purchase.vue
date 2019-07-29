<template>
  <div class="form-horizontal padding center" enctype="multipart/form-data">
    <div class="is-1 title">Kjøp Billetter</div>
      <div>
          <p class="is-italic">Billettene til BetaLAN er forhåndskjøpte og du som kunden vil ikke motta tjenesten før BetaLAN er gjennomført i sin helhet. Ved å klikke kjøp nederst på siden vil du automatisk godta disse vilkårene og 
              <router-link to="/salgsbetingelser" class="has-text-info">
              kjøpsavtalen
              </router-link> 
              vår. </p>
      </div>

      <!-- Select Basic -->
      <div class="field">
        <label class="label" for="numSeats">Antall Plasser</label>
        <div class="control">
          <div class="select">
            <select id="numSeats" name="numSeats" class="">
              <option value="1">1</option>
              <option value="2">2</option>
              <option value="3">3</option>
              <option value="4">4</option>
            </select>
        </div>
        </div>
      </div>

      <!-- Button -->
      <div class="field">
        <label class="label" for="buy"></label>
        <div class="control">
          <button id="buy" name="buy" class="button is-primary">Kjøp</button>
        </div>
      </div>

    </div>
  </div>

</template>

<script>
import axios from "axios";

export default {
  data() {
    return {};
  },
  methods: {
    addPost() {
      var token = localStorage.getItem("token");
      var title = document.querySelector("input[name=blogTitle]").value;
      var summary = document.querySelector("input[name=blogDescription]").value;
      var content = document.querySelector("textarea[name=blogContent]").value;

      var config = {
        headers: { Authorization: "bearer " + token }
      };

      var bodyParamters = {
        title: title,
        summary: summary,
        content: content
      };
      var self = this;
      axios
        .post("/api/event/add", bodyParamters, config)
        .then(function(response) {
          console.log(response.data);
          self.$router.push("/blog/detailed/" + response.data);
        })
        .catch(function(error) {
          console.log(error);
        });
    }
  }
};
</script>
