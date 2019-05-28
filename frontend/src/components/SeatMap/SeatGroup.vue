<template>
  <div class="seatgroupdiv">
    <VueDragResize
      class="seatgroup"
      :isActive="true"
      :snapToGrid="true"
      :isResizable="false"
      :gridX="10"
      :gridY="10"
      :w="group.rows*30"
      :h="group.columns*30"
      :x="400"
      :y="200"
      :parentW="900"
      :parentH="900"
      :parentLimitation="true   "
      style="background-color: yellow;"
      v-on:dragging="resize"
    >
      <div v-for="seat in group.seats" v-bind:key="seat">
        <Seat v-bind:seat="seat"></Seat>
      </div>
    </VueDragResize>
  </div>
</template>

<script>
import VueDragResize from "@/components/SeatMap/vue-drag-resize.vue";
import Seat from "@/components/SeatMap/Seat.vue";
export default {
  components: {
    Seat: Seat,
    VueDragResize
  },
  props: {
    group: [],
    seats: []
  },
  created() {
    //alert(JSON.stringify(this.group, null, 2));
    //alert(JSON.stringify(this.seats, null, 2));
  },
  methods: {
    resize(newRect) {
      var pos = {};
      pos.width = newRect.width;
      pos.height = newRect.height;
      pos.top = newRect.top;
      pos.left = newRect.left;
    }
  }
};
</script>

<style>
</style>
