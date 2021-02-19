using OpenRA.MiniYamlParser;
using Xunit;

namespace OpenRa.Tests
{
    public class ReaderTests
    {
        [Fact]
        public void Should_read_key()
        {
            var reader = new MiniYamlReader("key1:");
            reader.Read();

            Assert.Equal(MiniYamlToken.Key, reader.CurrentToken);
            Assert.Equal(4, reader.Value.Length);
            Assert.Equal("key1", reader.Value.ToString());
        }

        [Fact]
        public void Should_read_key_value()
        {
            var reader = new MiniYamlReader("key1:value");
            reader.Read();

            Assert.Equal(MiniYamlToken.Key, reader.CurrentToken);
            Assert.Equal(4, reader.Value.Length);
            Assert.Equal("key1", reader.Value.ToString());


            reader.Read();

            Assert.Equal(MiniYamlToken.String, reader.CurrentToken);
            Assert.Equal(5, reader.Value.Length);
        }

        [Fact]
        public void Should_handle_whitespace_start_of_value()
        {
            var reader = new MiniYamlReader("key1: value");
            reader.Read();

            Assert.Equal(MiniYamlToken.Key, reader.CurrentToken);
            Assert.Equal(4, reader.Value.Length);

            reader.Read();

            Assert.Equal(MiniYamlToken.String, reader.CurrentToken);
            Assert.Equal(5, reader.Value.Length);

        }

        [Fact]
        public void Should_handle_newline()
        {
            var yaml = @"key1: value
key_2: value_2";
            var reader = new MiniYamlReader(yaml);
            reader.Read();

            Assert.Equal(MiniYamlToken.Key, reader.CurrentToken);
            Assert.Equal(4, reader.Value.Length);
            Assert.Equal("key1", reader.Value.ToString());


            reader.Read();

            Assert.Equal(MiniYamlToken.String, reader.CurrentToken);
            Assert.Equal(5, reader.Value.Length);
            Assert.Equal("value", reader.Value.ToString());



            Assert.True(reader.Read());

            Assert.Equal(MiniYamlToken.Key, reader.CurrentToken);
            Assert.Equal(5, reader.Value.Length);
            Assert.Equal("key_2", reader.Value.ToString());


            reader.Read();

            Assert.Equal(MiniYamlToken.String, reader.CurrentToken);
            Assert.Equal(7, reader.Value.Length);
            Assert.Equal("value_2", reader.Value.ToString());


        }


        [Fact]
        public void Should_handle_comment()
        {
            var yaml = @"#comment
key_1: value_1";
            var reader = new MiniYamlReader(yaml);
            reader.Read();

            Assert.Equal(MiniYamlToken.Key, reader.CurrentToken);
            Assert.Equal("key_1", reader.Value.ToString());

            reader.Read();

        }


        [Fact]
        public void Should_handle_comment_after_value_with_space()
        {
            var yaml = @"key1:value #test";
            var doc = new MiniYamlReader(yaml);
            doc.Read();

            Assert.Equal(MiniYamlToken.Key, doc.CurrentToken);
            Assert.Equal(4, doc.Value.Length);

            doc.Read();


            Assert.Equal(MiniYamlToken.String, doc.CurrentToken);
            Assert.Equal(5, doc.Value.Length);
        }

        [Fact]
        public void Thest2_comment2()
        {
            var yaml = @"key1: value#comment
key_2: value_2";
            var doc = new MiniYamlReader(yaml);
            doc.Read();

            Assert.Equal(MiniYamlToken.Key, doc.CurrentToken);
            Assert.Equal("key1", doc.Value.ToString());

            doc.Read();

            Assert.Equal(MiniYamlToken.String, doc.CurrentToken);
            Assert.Equal("value", doc.Value.ToString());

            Assert.True(doc.Read());

            Assert.Equal(MiniYamlToken.Key, doc.CurrentToken);
            Assert.Equal("key_2", doc.Value.ToString());

            doc.Read();

            Assert.Equal(MiniYamlToken.String, doc.CurrentToken);
            Assert.Equal(7, doc.Value.Length);

        }

    }
}