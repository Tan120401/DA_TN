<template>
  <div>
    <Line :options="options" :data="data" style="width: 100%; height: 450px" />
  </div>
</template>

<script>
import { Line } from "vue-chartjs";
import {
  CategoryScale,
  Chart as ChartJS,
  Legend,
  LineElement,
  LinearScale,
  PointElement,
  Title,
  Tooltip,
} from "chart.js";
ChartJS.register(
  Title,
  Tooltip,
  Legend,
  LineElement,
  CategoryScale,
  LinearScale,
  PointElement
);
export default {
  name: "DonutChart",
  components: { Line },
  props: {
    label: {
      type: Array,
      default: [],
    },
    data: {
      type: Array,
      default: [],
    },
  },
  setup(props) {
    return {
      data: {
        labels: Object.values(props.label),
        datasets: [
          {
            label: "Thống kê doanh thu theo tháng",
            backgroundColor: "#8bbdcf",
            data: props.data,
          },
        ],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
      },
    };
  },
};
</script>