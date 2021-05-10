import javax.swing.SwingUtilities;
import java.util.Arrays;
import java.util.stream.IntStream;


class KnapsackMain {
    public static void main(String[] args) {
        var items = randomizeItems(8, 12345, 1, 29, 1, 29);
        var solution = solve(15, items);
        AppWindow window = new AppWindow();
        window.buildGUI();
        for (var item: solution) {
            System.out.println(item.toString());
        }
    }

    // Możesz dodać slidery dla ustawiania max_w i max_v,
    // z tym na pewno dostaniemy 5.0
    // Wcześniej mieliśmy zakres 1-29 dla obu wartości
    public static Item[] randomizeItems(int num_items, long seed, int min_w, int max_w, int min_v, int max_v) {
        var rng = new RandomNumberGenerator(seed);

        return IntStream.range(0, num_items)
            .mapToObj(i -> new Item(rng.nextInt(min_w, max_w), rng.nextInt(min_v, max_v)))
            .toArray(Item[]::new);
    }

    public static Item[] solve(int capacity, Item[] items) {
        Item[] items_sorted = items;

        // Sort by value/weight ratio descending
        Arrays.sort(items_sorted, (x, y) -> {
            return x.getValueToWeightRatio() < y.getValueToWeightRatio() ? 1 : -1;
        });

        int w, drop_index = 0;

        while ((w = items_sorted[drop_index].getWeight()) <= capacity) {
            capacity -= w;
            drop_index++;
        }

        return Arrays.copyOfRange(items_sorted, 0, drop_index);
    }
}


class Item {
    private int weight;
    private int value;

    public Item(int weight, int value) {
        this.weight = weight;
        this.value = value;
    }

    public String toString() {
        return String.format("Item(W=%d, V=%d)", weight, value);
    }

    public int getWeight() {
        return weight;
    }

    public int getValue() {
        return value;
    }

    public double getValueToWeightRatio() {
        return (double) value / weight;
    }
}
