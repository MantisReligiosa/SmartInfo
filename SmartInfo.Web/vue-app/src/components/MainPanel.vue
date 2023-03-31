<template>
  <v-app-bar
      color="primary"
  >
    <v-menu
        open-on-hover
    >
      <template
          v-slot:activator="{ props }"
      >
        <v-app-bar-nav-icon
            variant="text"
            v-bind="props"
        ></v-app-bar-nav-icon>
      </template>
      <v-list>
        <v-list :items="mainMenu"></v-list>
      </v-list>
    </v-menu>
    <v-item-group
        selected-class="bg-blue"
        mandatory>
      <v-item
          v-slot="{ selectedClass, toggle }">
        <v-btn
            size="x-large"
            :class="[selectedClass, asDefault?'bg-blue':'']"
            @click="switchToSelectionMode(toggle)"
        >
          <v-icon
              icon="mdi-navigation-variant-outline"
              class="mdi-flip-h"
              size="x-large"
          ></v-icon>
          <v-tooltip
              activator="parent"
              location="bottom"
          >{{ tooltipSelect }}
          </v-tooltip>
        </v-btn>
      </v-item>
      <v-item
          v-slot="{ selectedClass, toggle }">
        <v-menu>
          <template
              v-slot:activator="{ props }">
            <v-btn
                size="x-large"
                v-bind="props"
                :class="selectedClass"
            >
              <v-icon
                  :icon="blockMenuIcon"
                  size="x-large"
              ></v-icon>
              <v-icon
                  icon="mdi-chevron-down"
              ></v-icon>
              <v-tooltip
                  activator="parent"
                  location="bottom"
              >{{ tooltipBlocks }}
              </v-tooltip>
            </v-btn>
          </template>
          <v-list>
            <v-list-item
                v-for="b in blocksMenu"
                :key="b"
                @click="switchToDrawMode(b, toggle)"
            >
              <v-icon
                  class="mr-2"
                  :icon="b.props.prependIcon"
              ></v-icon>
              <v-label>
                {{ b.title }}
              </v-label>
            </v-list-item>
          </v-list>
        </v-menu>
      </v-item>
    </v-item-group>
    <v-spacer></v-spacer>
    <v-menu>
      <template v-slot:activator="{ props }">
        <v-btn
            size="x-large"
            v-bind="props"
        >
          {{ selectedDevice.name }}
          <v-icon
              icon="mdi-chevron-down"
          ></v-icon>
        </v-btn>
      </template>
      <v-list>
        <v-list-item
            v-for="(device, index) in devices"
            @click="selectDeviceById(device.id)"
            :title="device.name"
            :key="index"
        ></v-list-item>
      </v-list>
    </v-menu>
    <v-spacer></v-spacer>
    <v-btn
        variant="plain"
    >
      <v-icon
          size="x-large"
          icon="mdi-play"
      ></v-icon>
      <v-tooltip
          activator="parent"
          location="bottom"
      >{{ tooltipPlay }}
      </v-tooltip>
    </v-btn>
    <v-btn
        variant="plain"
    >
      <v-icon
          size="x-large"
          icon="mdi-stop"
      ></v-icon>
      <v-tooltip
          activator="parent"
          location="bottom"
      >{{ tooltipStop }}
      </v-tooltip>
    </v-btn>
    <v-btn
        variant="plain"
    >
      <v-icon
          size="x-large"
          icon="mdi-refresh"
      ></v-icon>
      <v-tooltip
          activator="parent"
          location="bottom"
      >{{ tooltipRefresh }}
      </v-tooltip>
    </v-btn>
  </v-app-bar>
</template>

<script>
import {blockType, blockIcon} from '@/constants'
import {mainStore} from '@/store/mainStore'
import {deviceStore} from '@/store/deviceStore'

export default {
  data: () => ({
    tooltipSelect: 'Выбор',
    tooltipBlocks: 'Блоки',
    tooltipPlay: 'Пуск',
    tooltipStop: 'Стоп',
    tooltipRefresh: 'Перезагрузить',
    mainMenu: [
      {
        title: 'Item #1',
        value: 1,
      },
      {
        title: 'Item #2',
        value: 2,
      },
      {
        title: 'Item #3',
        value: 3,
      },
    ],
    blockMenuIcon: blockIcon.text,
    blocksMenu: [
      {
        title: 'Текст',
        value: blockType.text,
        props: {
          prependIcon: blockIcon.text,
        },

      },
      {
        title: 'Таблица',
        value: blockType.table,
        props: {
          prependIcon: blockIcon.table,
        },
      },
      {
        title: 'Изображение',
        value: blockType.picture,
        props: {
          prependIcon: blockIcon.picture,
        },
      },
      {
        title: 'ДатаВремя',
        value: blockType.dateTime,
        props: {
          prependIcon: blockIcon.dateTime,
        },
      },
      {
        title: 'Сценарий',
        value: blockType.scenario,
        props: {
          prependIcon: blockIcon.scenario,
        },
      },
    ],
    asDefault: true
  }),
  methods: {
    async selectDeviceById(deviceId) {
      mainStore().selectDeviceById(deviceId)
      await deviceStore().loadById(deviceId)
    },
    switchToSelectionMode(toggle) {
      this.asDefault = false
      mainStore().switchToSelectionMode()
      toggle()
    },
    switchToDrawMode(item, toggle) {
      this.blockMenuIcon = item.props.prependIcon
      this.asDefault = false
      mainStore().switchToDrawMode(item.value)
      toggle()
    }
  },
  computed: {
    devices() {
      return mainStore().devices
    },
    selectedDevice() {
      return mainStore().device
    }
  }
}
</script>

<style scoped>

</style>