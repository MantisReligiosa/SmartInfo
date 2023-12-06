<script setup lang="ts">
export interface IRowHeightChangedPayload {
  index: number,
  value: number
}

import {ITableSizeUnit} from "@/interfaces/Blocks";
import {defineProps, defineEmits, ref, computed} from "vue";

interface IProps {
  rowHeights: ITableSizeUnit[]
}

const length = computed(() => props.rowHeights?.length ?? 0)

const rowNumber = ref(1)

const props = defineProps<IProps>()

const emit = defineEmits<{
  (e: 'valueChanged', payload: IRowHeightChangedPayload): void
}>()

const captionByIndex = computed(() => rowNumber?.value ?? '')

const valueByIndex = computed(() => {
  if (!props.rowHeights)
    return ''
  const row = props.rowHeights.find(c => c.index == rowNumber.value - 1)
  if (!row)
    return ''
  return row.value
})

const next = () => {
  rowNumber.value = rowNumber.value >= length.value
      ? 1
      : rowNumber.value + 1
}
const prev = () => {
  rowNumber.value = rowNumber.value <= 1
      ? length.value
      : rowNumber.value - 1
}

const updateValue = (valueStr: string) => {
  const value = Number.parseInt(valueStr)
  emit('valueChanged', {index: rowNumber.value - 1, value})
}

const selectIndex = (i: number, t: () => void) => {
  rowNumber.value = i
  t()
}

</script>

<template>
  <v-card
      flat
      rounded="0"
  >
    <v-window
        v-model="rowNumber"
    >
      <v-window-item
          v-for="w in length"
          :key="w"
          :value="w"
      >
        <v-container
            class="d-flex justify-center align-center"
        >
          <v-row>
            <v-col>
              <v-label
                  :text="'Строка ' + captionByIndex"
                  class="mt-3"
              ></v-label>
            </v-col>
            <v-col>
              <v-text-field
                  hide-details
                  single-line
                  type="number"
                  @update:modelValue="updateValue"
                  :modelValue="valueByIndex"
                  min="0"
                  density="compact"
                  variant="underlined"
                  prepend-icon="mdi-alpha-w"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-container>
      </v-window-item>
    </v-window>

    <v-card-actions
        class="justify-space-between pa-0"
    >
      <v-btn
          variant="plain"
          icon="mdi-chevron-left"
          @click.stop="prev"
      ></v-btn>
      <v-item-group
          v-model="rowNumber"
          class="text-center"
          mandatory
      >
        <v-item
            v-for="n in length"
            :key="`btn-${n}`"
            v-slot="{ isSelected, toggle }"
            :value="n"
        >
          <v-btn
              :variant="isSelected ? 'tonal' : 'text'"
              :color="isSelected ? 'info' : 'black'"
              icon="mdi-record"
              size="x-small"
              style="max-width: 15px; max-height: 15px"
              @click.stop="selectIndex(n, toggle)"
          ></v-btn>
        </v-item>
      </v-item-group>
      <v-btn
          variant="plain"
          icon="mdi-chevron-right"
          @click.stop="next"
      ></v-btn>
    </v-card-actions>
  </v-card>

</template>

<style scoped>

</style>