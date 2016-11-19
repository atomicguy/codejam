# codejam

Install Magenta/MIDI interfaces:


#### Automated Install of Magenta

If you are running Mac OS X or Ubuntu, you can try using our automated
installation script. Just paste the following command into your terminal.

```
curl https://raw.githubusercontent.com/tensorflow/magenta/master/magenta/tools/magenta-install.sh > /tmp/magenta-install.sh
bash /tmp/magenta-install.sh
```

### Install RtMidi

The interface uses a python library called [mido](http://mido.readthedocs.io) to
interface your computer's MIDI hub. For it to work, you need to separately
install a backend library it can use to connect to your system. Below are
instructions for installing RtMidi. Note that if you used our
[installer script](/README.md#automated-install), RtMidi will already be
installed.

**Mac:**

```bash
pip install --pre python-rtmidi
```

### Connect/Install MIDI Controller

If you are using a hardware controller, attach it to the machine. If you do not
have one, you can install a software controller such as
[VMPK](http://vmpk.sourceforge.net/) by doing the following.

**Ubuntu:** Use the command `sudo apt-get install vmpk`.<br />
**Mac:** Download and install from the
[VMPK website](http://vmpk.sourceforge.net/#Download).

If using FluidSynth, you will also want to install a decent soundfont. You can
install one by doing the following:

**Ubuntu:** Use the command `sudo apt-get install fluid-soundfont-gm`.<br />
**Mac:** Download the soundfont from
http://www.musescore.org/download/fluid-soundfont.tar.gz and unpack the SF2
file.

If using a software controller (e.g., VMPK), launch it.

If using a software synth, launch it. Launch FluidSynth with the
recommended soundfont downloaded above using:

```bash
fluidsynth /path/to/sf2
```

## Launching the Interface

After completing the installation and set up steps above have the interface list
the available MIDI ports:

```bash
magenta_midi --list_ports
```

You should see a list of available input and output ports, including both the
controller (e.g., "VMPK Output") and synthesizer (e.g., "FluidSynth virtual
port").


### Generate a melody

```
BUNDLE_PATH=<absolute path of .mag file>
CONFIG=<one of 'basic_rnn', 'lookback_rnn', or 'attention_rnn'>

melody_rnn_generate \
--config=${CONFIG} \
--bundle_file=${BUNDLE_PATH} \
--output_dir=/tmp/melody_rnn/generated \
--num_outputs=10 \
--num_steps=128 \
--primer_melody="[60]"
```

This will generate a melody starting with a middle C. If you'd like, you can also supply a priming melody using a string representation of a Python list. The values in the list should be ints that follow the melodies_lib.Melody format (-2 = no event, -1 = note-off event, values 0 through 127 = note-on event for that MIDI pitch). For example `--primer_melody="[60, -2, 60, -2, 67, -2, 67, -2]"` would prime the model with the first four notes of *Twinkle Twinkle Little Star*. Instead of using `--primer_melody`, we can use `--primer_midi` to prime our model with a melody stored in a MIDI file. For example, `--primer_midi=<absolute path to magenta/models/melody_rnn/primer.mid>` will prime the model with the melody in that MIDI file.
