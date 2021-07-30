using System;
using System.Text;

namespace Danmakux
{
    public class MotionHelper
    {
        private StringBuilder _publicBuilder = null;
        private StringBuilder _backupBuilder = new StringBuilder();
        private StringBuilder _pathBuilder = null;//new StringBuilder();
        private string _dstContainer = "";
        private string _dstBackup = "";
        private string _dstPath = "";
        private bool _isFirst = true;
        private bool _isBackupFirst = true;
        private bool _allBackupLayerRequired = false;

        private bool _isBackupManual = false;
        //private bool _pathTransformRequired = false;

        public MotionHelper(StringBuilder builder, string dstContainer, string dstBackup, string dstPath)
        {
            _publicBuilder = builder;
            _dstContainer = dstContainer;
            _dstBackup = dstBackup;
            _dstPath = dstPath;
        }
/*
        private MotionHelper ApplyPath(float duration, TextProperty prop = null, string motion = "linear")
        {
            var builder = _pathBuilder;
            bool isFirst = _isFirst;
            if (!_isFirst)
                builder.Append("then ");
            _isFirst = false;
            builder.Append($"set {_dstPath} {{");
            bool layerRequired = false;
            for (;;)
            {
                if (prop == null) break;
                if (prop.width != null)
                {
                    builder.Append($"width={prop.width}%,");
                    prop.width = null;
                    layerRequired = true;
                }

                if (prop.height != null)
                {
                    builder.Append($"height={prop.height}%,");
                    prop.height = null;
                    layerRequired = true;
                }
                break;
            }

            builder.Append($"}} {duration}s");
            if (motion != "linear" && layerRequired)
            {
                builder.Append($",\"{motion}\"");
            }
            builder.Append($"\n");

            if (layerRequired)
                _pathTransformRequired = true;
            
            return this;
        }
        */
        
        public MotionHelper Apply(float duration,  TextProperty prop = null, string motion = "linear", bool isBackup = false)
        {
            //set b_3_1 {} 0.1s then set b_3_1 {x = 20%, y = 0%, rotateY = 0, alpha = 1} 1s, "ease-out" then set b_3_1{} 2s
            //then set b_3_1 {x = 20%, y = 150%, rotateY = 30, alpha = 0} 2s, "ease-in"
            if (Math.Abs(duration - 999) < 0.1 && isBackup)
            {
                _isBackupManual = true;
                return this;
            }

            var builder = isBackup ? _backupBuilder : _publicBuilder;
            bool isFirst = isBackup ? _isBackupFirst : _isFirst;
            if (!isFirst)
                builder.Append("then ");
            if (isBackup)
                _isBackupFirst = false;
            else
                _isFirst = false;
            builder.Append($"set {(isBackup ? _dstBackup : _dstContainer)} {{");
            bool backupLayerRequired = false;
            for (;;)
            {
                if (prop == null) break;
                if (prop.x != null)
                {
                    //加 50 的原因参见 GraphicHelper对应部分
                    builder.Append($"x={prop.x + 50}%,");
                    prop.x = null;
                    backupLayerRequired = true;
                }

                if (prop.y != null)
                {
                    //加 50 的原因参见 GraphicHelper对应部分
                    builder.Append($"y={prop.y + 50}%,");
                    prop.y = null;
                    backupLayerRequired = true;
                }

                if (prop.rotateX != null && !backupLayerRequired)
                {
                    builder.Append($"rotateX={prop.rotateX},");
                    prop.rotateX = null;
                    backupLayerRequired = true;
                }

                if (prop.rotateY != null && !backupLayerRequired)
                {
                    builder.Append($"rotateY={prop.rotateY},");
                    prop.rotateY = null;
                    backupLayerRequired = true;
                }

                if (prop.rotateZ != null && !backupLayerRequired)
                {
                    builder.Append($"rotateZ={prop.rotateZ},");
                    prop.rotateX = null;
                    backupLayerRequired = true;
                }

                if (prop.scale != null && !backupLayerRequired)
                {
                    builder.Append($"scale={prop.scale},");
                    prop.scale = null;
                    backupLayerRequired = true;
                }

                if (prop.zIndex != null)
                {
                    builder.Append($"zIndex={prop.zIndex},");
                    prop.zIndex = null;
                }
                //if (prop.duration != null) _builder.Append($",duration={prop.duration}s");
                //if (prop.borderAlpha != null) _builder.Append($" borderAlpha={prop.borderAlpha}");
                //if (prop.borderWidth != null) _builder.Append($" borderWidth={prop.borderWidth}");
                //if (!string.IsNullOrEmpty(prop.borderColor)) _builder.Append($" borderColor={prop.borderColor}");
                //if (!string.IsNullOrEmpty(prop.fillColor)) _builder.Append($" fillColor={prop.fillColor}");
                //if (prop.fillAlpha != null) _builder.Append($" borderWidth={prop.fillAlpha}");

                if (prop.alpha != null)
                {
                    builder.Append($"alpha={prop.alpha},");
                    prop.alpha = null;
                }
                break;
            }

            builder.Append($"}} {duration}s");
            if (motion != "linear")
            {
                builder.Append($",\"{motion}\"");
            }
            builder.Append($"\n");

            if (!isBackup && !_isBackupManual)
            {
                if (prop!= null && 
                    (prop.rotateX != null || prop.rotateY != null || prop.rotateZ != null || prop.scale != null))
                    _allBackupLayerRequired = true;
                Apply(duration, prop, motion, true);
                
                
                //_isFirst = isFirst;
                //ApplyPath(duration, prop, motion);
            }
            

            return this;
        }

        public void ForceSetBackup(bool value)
        {
            _allBackupLayerRequired = value;
        }

        public void ProcessBackupLayer()
        {
            if (_allBackupLayerRequired)
            {
                if (string.IsNullOrEmpty(_dstBackup))
                    throw new Exception();
                _publicBuilder.Append(_backupBuilder.ToString());
            }
/*
            if (_pathTransformRequired)
            {
                _publicBuilder.Append(_pathBuilder.ToString());
            }
            */
        }
    }
}