import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.event.ChangeEvent;
import javax.swing.event.ChangeListener;


public class AppWindow implements ActionListener {
    JFrame frame;
    JButton countButton;
    JTextField textF;
    JLabel resultJL;
    JTextField tf1;

    int seed =12345;
    int sumWeight;
    int sumValue;
    Item[] items = KnapsackMain.randomizeItems(8, 12345, 1, 29, 1, 29);
    Item[] solution = KnapsackMain.solve(15, items);

    public void buildGUI() {
        solve.main();

        frame = new JFrame();
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setSize(400, 200);
        //Input text field
        tf1=new JTextField();
        tf1.setBounds(50,50,150,20);
        frame.add(tf1);
        //Button
        countButton = new JButton();
        countButton.setText("Oblicz!");
        countButton.addActionListener(this);
        //Panel
        JPanel jp = new JPanel();
        jp.setLayout(new GridLayout(1,3));
        jp.add(countButton);
        jp.add(tf1);
        JPanel jp2 = new JPanel();
        jp2.setLayout(new GridLayout(3,1));
        //Text field
        textF = new JTextField("Podaj ziarno do wylosowania wartości i wag przedmiotów");
        resultJL = new JLabel();
        resultJL.setSize(400,20);
        resultJL = new JLabel();
        resultJL.setSize(400,20);
        jp2.add(textF);
        jp2.add(resultJL);
        jp2.add(jp);
        frame.add(jp2);
        frame.setVisible(true);
    }

    @Override
    public void actionPerformed(ActionEvent action) {
        String s1=tf1.getText();
        seed=Integer.parseInt(s1);
        if (action.getSource() == countButton) {
            resultJL.setText("");
            //Making solution
            items = KnapsackMain.randomizeItems(8, seed, 1, solve.b.getValue(), 1, solve.c.getValue());
            solution = KnapsackMain.solve(15, items);

            for (var item : solution) {
                sumWeight += item.getWeight();
                sumValue += item.getValue();
            }
            //Printing solution
            for (var item : solution) {
                this.resultJL.setText("<html><center>" + resultJL.getText() + item.toString() + "<br>");
            }
                this.resultJL.setText(this.resultJL.getText()+"<html><center>"+"Sumaryczna waga przedmiotów:"+ sumWeight + "<br>"+
                        " Sumaryczna wartość przedmiotów:"+ sumValue + "<br>");
            sumValue = 0;
            sumWeight = 0;
        }
        else
            System.out.println("Ups");
    }

    static class solve extends JFrame implements ChangeListener {

        // frame
        static JFrame f;

        // slider
        static JSlider b,c;

        // label
        static JLabel l,l1;

        // main class
        public static void main()        {
            // create a new frame
            f = new JFrame("Wybór zakresu wag oraz wartości");

            // create a object
            solve s = new solve();

            // create label
            l = new JLabel();
            l1 = new JLabel();

            // create a panel
            JPanel p = new JPanel();

            // create a slider
            b = new JSlider(1, 29, 19);
            c = new JSlider(1, 29, 19);

            // paint the ticks and tracks
            b.setPaintTrack(true);
            b.setPaintTicks(true);
            b.setPaintLabels(true);
            c.setPaintTrack(true);
            c.setPaintTicks(true);
            c.setPaintLabels(true);
            // set spacing
            b.setMajorTickSpacing(28);
            b.setMinorTickSpacing(1);
            c.setMajorTickSpacing(28);
            c.setMinorTickSpacing(1);
            // setChangeListener
            b.addChangeListener(s);
            c.addChangeListener(s);
            // add slider to panel
            p.add(b);
            p.add(c);
            p.add(l);
            p.add(l1);

            f.add(p);

            // set the text of label
            l.setText("<html><center>" + "Maksymalna waga przedmiotu =" + b.getValue() + "<br>");
            l1.setText("<html><center>" + "Maksymalna wartość przedmiotu =" + c.getValue() + "<br>");

            // set the size of frame
            f.setSize(300, 300);

            f.show();
        }

        // if JSlider value is changed
        public void stateChanged(ChangeEvent e)
        {
            l.setText("<html><center>" + "Maksymalna waga przedmiotu =" + b.getValue() + "<br>");
            l1.setText("<html><center>" + "Maksymalna wartość przedmiotu =" + c.getValue() + "<br>");

        }
    }
}