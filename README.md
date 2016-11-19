# codejam

Install Magenta/MIDI interfaces:


## Automated Install of Magenta

If you are running Mac OS X or Ubuntu, you can try using our automated
installation script. Just paste the following command into your terminal.

```
curl https://raw.githubusercontent.com/tensorflow/magenta/master/magenta/tools/magenta-install.sh > /tmp/magenta-install.sh
bash /tmp/magenta-install.sh
pip install --pre python-rtmidi
```

## Connect/Install MIDI Controller

If you are using a hardware controller, attach it to the machine. If you do not
have one, you can install a software controller such as
[VMPK](http://vmpk.sourceforge.net/) by doing the following.

**Ubuntu:** Use the command `sudo apt-get install vmpk`

**Mac:** Download and install from the
[VMPK website](http://vmpk.sourceforge.net/#Download).

## Launching the Interface

After completing the installation and set up steps above have the interface list
the available MIDI ports:

```
source activate magenta
magenta_midi --list_ports
```

You can test the interface using VMPK, by connecting the two together:

```
magenta_midi --input_port="VMPK Output" --output_port="VMPK Input" --bundle_file=magenta-models/attention_rnn.mag   --phrase_bars=4
```

## Creating a network session

In order to have a jam together setup a midi network session and connect
the neural net to it:

```
magenta_midi --input_port="Network neural" --output_port="Network neural" --bundle_file=magenta-models/attention_rnn.mag   --phrase_bars=4

```
