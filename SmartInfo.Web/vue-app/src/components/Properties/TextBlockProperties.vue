<template>
  <v-container>
      <v-text-field
              variant="underlined"
              :model-value="block.text"
              @update:model-value="setText"
      ></v-text-field>
  </v-container>
</template>

<script setup>
import {deviceStore} from "@/store/deviceStore";
import {computed} from "vue";

const dStore = deviceStore()
const block = computed(() => dStore.block)

function setText(value) {
    block.value.text = value
}
</script>

<style scoped>

</style>