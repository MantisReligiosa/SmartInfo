<template>
  <v-container>
    <v-row>
      <v-text-field
          class="centered-input"
          density="compact"
          variant="underlined"
          v-model="block.caption"
          spellcheck="false"
      ></v-text-field>
    </v-row>
    <v-row>
      <v-col>
        <v-text-field
            hide-details
            single-line
            type="number"
            v-model="block.x"
            min="0"
            density="compact"
            variant="underlined"
            prepend-icon="mdi-alpha-x"
        ></v-text-field>
      </v-col>
      <v-col>
        <v-text-field
            hide-details
            single-line
            type="number"
            v-model="block.y"
            min="0"
            density="compact"
            variant="underlined"
            prepend-icon="mdi-alpha-y"
        ></v-text-field>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-text-field
            hide-details
            single-line
            type="number"
            v-model="block.h"
            min="0"
            density="compact"
            variant="underlined"
            prepend-icon="mdi-alpha-h"
        ></v-text-field>
      </v-col>
      <v-col>
        <v-text-field
            hide-details
            single-line
            type="number"
            v-model="block.w"
            min="0"
            density="compact"
            variant="underlined"
            prepend-icon="mdi-alpha-w"
        ></v-text-field>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import {deviceStore} from '@/store/deviceStore'

export default {
  name: "BlockProperties",
  data: () => ({
    currentBlockId:0
  }),
  computed: {
    block() {
      return deviceStore().block
    }
  }
}
</script>

<style scoped>
:deep(.v-input__prepend){
  margin-right: 0!important;
}
:deep(.v-col){
  padding:0!important;
}
.centered-input >>> input {
  text-align: center
}
</style>